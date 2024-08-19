using Newtonsoft.Json;
using System.Text;
using UPBank.Address.API.Models;
using UPBank.Utils.CrossCutting.Exception.Contracts;
using UPBank.Utils.Integration.Address.Contracts;

namespace UPBank.Utils.Integration.Address.Services
{
    public class AddressService : IAddressService
    {
        private static readonly HttpClient _client = new HttpClient();
        private readonly IDomainNotificationService _domainNotificationService;

        public async Task<T?> ExecuteTryCatchAsync<T>(Func<Task<T>> func, string type)
        {
            try
            {
                return await func();
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao conectar com a API de" + type + ": " + e);

                return default;
            }
        }

        public async Task<OutputAddressModel?> CreateAddress(InputAddressModel createAddress)
        {
            return await ExecuteTryCatchAsync(async () =>
                {
                    var content = new StringContent(JsonConvert.SerializeObject(createAddress), Encoding.UTF8, "application/json");
                    var response = await _client.PostAsync("https://localhost:7082/api/addresses", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        _domainNotificationService.Add("Houve um erro ao criar endereço: " + errorMessage);
                    }

                    var result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<OutputAddressModel>(result);
                }, "Address");
        }

        public async Task<OutputAddressModel?> GetCompleteAddressById(string id)
        {
            return await ExecuteTryCatchAsync(async () =>
            {
                var response = await _client.GetAsync($"https://localhost:7082/api/addresses/{id}");

                if (!response.IsSuccessStatusCode)
                    _domainNotificationService.Add("Houve um erro ao trazer o endereço: " + response.Content.ReadAsStringAsync());

                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<OutputAddressModel>(result);
            }, "Address");
        }

        public async Task<OutputAddressModel?> UpdateAddress(string id, InputAddressModel updateAddress)
        {
            return await ExecuteTryCatchAsync(async () =>
            {
                var content = new StringContent(JsonConvert.SerializeObject(updateAddress), Encoding.UTF8, "application/json");
                var response = await _client.PatchAsync($"https://localhost:7082/api/addresses/{id}", content);

                if (!response.IsSuccessStatusCode)
                {
                    _domainNotificationService.Add("Houve um erro ao atualizar o endereço: " + response.Content.ReadAsStringAsync());
                }

                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<OutputAddressModel>(result);
            }, "Address");
        }
    }
}