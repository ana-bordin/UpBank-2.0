using Newtonsoft.Json;
using System.Text;
using UPBank.Address.Domain.Commands.CreateAddress;
using UPBank.Address.Domain.Commands.UpdateAddress;
using UPBank.Utils.Address.Contracts;
using UPBank.Utils.CommonsFiles.Contracts.Services;

namespace UPBank.Utils.Address.Services
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

        public async Task<CreateAddressCommandResponse?> CreateAddress(CreateAddressCommand createAddressCommand, CancellationToken cancellationToken)
        {
            return await ExecuteTryCatchAsync(async () =>
                {
                    var content = new StringContent(JsonConvert.SerializeObject(createAddressCommand), Encoding.UTF8, "application/json");
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

        public async Task<CreateAddressCommandResponse?> GetCompleteAddressById(string id, CancellationToken cancellationToken)
        {
            return await ExecuteTryCatchAsync(async () =>
            {
                var response = await _client.GetAsync($"https://localhost:7082/api/addresses/{id}");

                if (!response.IsSuccessStatusCode)
                    _domainNotificationService.Add("Houve um erro ao trazer o endereço: " + response.Content.ReadAsStringAsync());

                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<CreateAddressCommandResponse>(result);
            }, "Address");
        }

        public async Task<CreateAddressCommandResponse?> UpdateAddress(string Id, UpdateAddressCommand updateAddressCommand, CancellationToken cancellationToken)
        {
            return await ExecuteTryCatchAsync(async () =>
            {
                var content = new StringContent(JsonConvert.SerializeObject(updateAddressCommand), Encoding.UTF8, "application/json");
                var response = await _client.PatchAsync($"https://localhost:7082/api/addresses/{updateAddressCommand.Id}", content);

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