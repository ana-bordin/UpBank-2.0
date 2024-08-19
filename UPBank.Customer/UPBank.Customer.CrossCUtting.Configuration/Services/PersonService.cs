using Newtonsoft.Json;
using System.Text;
using UPBank.Person.Domain.Commands.CreatePerson;
using UPBank.Person.Domain.Commands.UpdatePerson;
using UPBank.Utils.CommonsFiles.Contracts.Services;
using UPBank.Utils.CrossCutting.Exeption;
using UPBank.Utils.Person.Contracts;

namespace UPBank.Utils.Person.Services
{
    public class PersonService : IPersonService
    {
        private static readonly HttpClient _client = new HttpClient();
        private readonly IDomainNotificationService _domainNotificationService;
        private readonly TryService _tryService;

        public async Task<CreatePersonCommandResponse?> CreatePersonAsync(CreatePersonCommand createPersonCommand)
        {
            return await _tryService.ExecuteTryCatchAsync(async () =>
            {
                var content = new StringContent(JsonConvert.SerializeObject(createPersonCommand), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("https://localhost:7048/api/peoples", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    _domainNotificationService.Add("Houve um erro ao criar a pessoa: " + errorMessage);
                }

                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<CreatePersonCommandResponse>(result);
            }, "Person");
        }

        public async Task<CreatePersonCommandResponse?> UpdatePersonAsync(string cpf, UpdatePersonCommand updatePersonCommand)
        {
            return await _tryService.ExecuteTryCatchAsync(async () =>
            {
                var content = new StringContent(JsonConvert.SerializeObject(updatePersonCommand), Encoding.UTF8, "application/json");
                var response = await _client.PutAsync($"https://localhost:7048/api/peoples/{cpf}", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    _domainNotificationService.Add("Houve um erro ao atualizar pessoa: " + errorMessage);
                }

                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<CreatePersonCommandResponse>(result);
            }, "Person");
        }

        public async Task<CreatePersonCommandResponse?> GetPersonByCPFAsync(string cpf)
        {
            return await _tryService.ExecuteTryCatchAsync(async () =>
            {
                var response = await _client.GetAsync($"https://localhost:7048/api/peoples/{cpf}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    _domainNotificationService.Add("Houve um erro ao buscar pessoa: " + errorMessage);
                }

                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<CreatePersonCommandResponse>(result);
            }, "Person");
        }
    }
}