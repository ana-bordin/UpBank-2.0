using Newtonsoft.Json;
using System.Text;
using UPBank.Address.Domain.Commands.CreateAddress;
using UPBank.Address.Domain.Commands.UpdateAddress;
using UPBank.Utils.CrossCutting.Exception.Contracts;
using UPBank.Utils.CrossCutting.Exception.Services;
using UPBank.Utils.Integration.Address.Contracts;

namespace UPBank.Utils.Integration.Address.Services
{
    public class AddressService : IAddressService
    {
        private static readonly HttpClient _client = new HttpClient();
        private readonly IDomainNotificationService _domainNotificationService;
        private readonly TryService _tryService;

        public AddressService(IDomainNotificationService domainNotificationService, TryService tryService)
        {
            _domainNotificationService = domainNotificationService;
            _tryService = tryService;
        }

        public async Task<CreateAddressCommandResponse?> CreateAddress(CreateAddressCommand createAddress)
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
                    return JsonConvert.DeserializeObject<CreateAddressCommandResponse>(result);
                }, "Address");
        }

        public async Task<CreateAddressCommandResponse?> GetCompleteAddressById(string id)
        {
            return await _tryService.ExecuteTryCatchAsync(async () =>
            {
                var response = await _client.GetAsync($"https://localhost:7082/api/addresses/{id}");

                if (!response.IsSuccessStatusCode)
                    _domainNotificationService.Add("Houve um erro ao trazer o endereço: " + response.Content.ReadAsStringAsync());

                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<CreateAddressCommandResponse>(result);
            }, "Address");
        }

        public async Task<CreateAddressCommandResponse?> UpdateAddress(string id, UpdateAddressCommand updateAddress)
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
                return JsonConvert.DeserializeObject<CreateAddressCommandResponse>(result);
            }, "Address");
        }
    }
}