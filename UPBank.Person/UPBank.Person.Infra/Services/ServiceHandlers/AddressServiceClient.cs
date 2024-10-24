using Newtonsoft.Json;
using System.Text;
using UPBank.Person.Domain.Commands.CreatePerson.Models.Address;
using UPBank.Person.Domain.Contracts.Services;
using UPBank.Utils.CrossCutting.Exception.Contracts;
using UPBank.Utils.CrossCutting.Exception.Services;

namespace UPBank.Person.Infra.Services.ServiceHandlers
{
    public class AddressServiceClient : IAddressServiceClient
    {
        private static readonly HttpClient _client = new HttpClient();
        private readonly IDomainNotificationService _domainNotificationService;
        private readonly TryService _tryService;

        public AddressServiceClient(IDomainNotificationService domainNotificationService, TryService tryService)
        {
            _domainNotificationService = domainNotificationService;
            _tryService = tryService;
        }

        public async Task<AddressResponse?> CreateAddress(AddressRequest createAddress)
        {
            return await _tryService.ExecuteTryCatchAsync(async () =>
            {
                var content = new StringContent(JsonConvert.SerializeObject(createAddress), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("https://localhost:7082/api/addresses", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    _domainNotificationService.Add("Houve um erro ao criar endereço: " + errorMessage);
                }

                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<AddressResponse>(result);
            }, "Address");
        }

        public async Task<AddressResponse?> GetCompleteAddressById(string id)
        {
            return await _tryService.ExecuteTryCatchAsync(async () =>
            {
                var response = await _client.GetAsync($"https://localhost:7082/api/addresses/{id}");

                if (!response.IsSuccessStatusCode)
                    _domainNotificationService.Add("Houve um erro ao trazer o endereço: " + response.Content.ReadAsStringAsync());

                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<AddressResponse>(result);
            }, "Address");
        }

        public async Task<AddressResponse?> UpdateAddress(string id, AddressRequest updateAddress)
        {
            return await _tryService.ExecuteTryCatchAsync(async () =>
            {
                var content = new StringContent(JsonConvert.SerializeObject(updateAddress), Encoding.UTF8, "application/json");
                var response = await _client.PatchAsync($"https://localhost:7082/api/addresses/{id}", content);

                if (!response.IsSuccessStatusCode)
                {
                    _domainNotificationService.Add("Houve um erro ao atualizar o endereço: " + response.Content.ReadAsStringAsync());
                }

                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<AddressResponse>(result);
            }, "Address");
        }
    }
}
