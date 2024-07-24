using Newtonsoft.Json;
using System.Text;
using UPBank.Person.Application.Models;
using UPBank.Utils.Person.Contracts;

namespace UPBank.Utils.Person.Services
{
    public class PersonService : IPersonService
    {
        private static readonly HttpClient _client = new HttpClient();
        public async Task<(bool ok, string message)> CreatePerson(PersonInputModel person)
        {

            var content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("https://localhost:7048/api/peoples", content);

            if (response.IsSuccessStatusCode)
                return (true, null);
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return (false, "houve um erro ao criar a pessoa: " + errorMessage);
            }

        }

        public async Task<bool> CheckIfExist(string cpf)
        {
            var response = await _client.GetAsync($"https://localhost:7048/api/peoples/{cpf}");

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<(PersonOutputModel person, string message)> GetPersonByCpf(string cpf)
        {
            var response = await _client.GetAsync($"https://localhost:7048/api/peoples/{cpf}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                return (JsonConvert.DeserializeObject<PersonOutputModel>(result), null);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return (null, "Houve um erro ao buscar pessoa: " + errorMessage);
            }
        }

        public async Task<(PersonOutputModel person, string message)> PatchPerson(string cpf, Models.DTOs.PersonPatchDTO personPatchDTO)
        {

            var content = new StringContent(JsonConvert.SerializeObject(personPatchDTO), Encoding.UTF8, "application/json");
            var response = _client.PatchAsync($"https://localhost:7048/api/peoples/{cpf}", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                return (JsonConvert.DeserializeObject<PersonOutputModel>(result), null);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return (null, "Houve um erro ao atualizar pessoa: " + errorMessage);
            }
        }
    }
}
