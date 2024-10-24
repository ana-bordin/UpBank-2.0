using Newtonsoft.Json;
using System.Text;
using UPBank.Customer.Domain.Contracts.Services;
using UPBank.Customer.Domain.Services;
using UPBank.Utils.CrossCutting.Exception.Contracts;
using UPBank.Utils.CrossCutting.Exception.Services;

namespace UPBank.Customer.Infra.Service
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

        public async Task<AddressResponseData?> CreateAddress(AddressRequestData createAddress)
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
                return JsonConvert.DeserializeObject<AddressResponseData>(result);
            }, "Address");
        }

        public async Task<AddressResponseData?> GetCompleteAddressById(string id)
        {
            return await _tryService.ExecuteTryCatchAsync(async () =>
            {
                var response = await _client.GetAsync($"https://localhost:7082/api/addresses/{id}");

                if (!response.IsSuccessStatusCode)
                    _domainNotificationService.Add("Houve um erro ao trazer o endereço: " + response.Content.ReadAsStringAsync());

                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<AddressResponseData>(result);
            }, "Address");
        }

        public async Task<AddressResponseData?> UpdateAddress(string id, AddressRequestData updateAddress)
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
                return JsonConvert.DeserializeObject<AddressResponseData>(result);
            }, "Address");
        }
    }
}
