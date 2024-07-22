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
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("https://localhost:7048/api/person", content);
                if (response.IsSuccessStatusCode)
                {
                    return (true, null);
                }
                else
                    return (false, "houve um erro ao criar a pessoa");
            }
            catch (Exception e)
            {
                return (false, "houve um erro:" + e);
            }
        }

        public async Task<bool> CheckIfExist(string cpf)
        {
            try
            {
                var response = await _client.GetAsync($"https://localhost:7048/api/person/{cpf}");
                if (response.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<UPBank.Person.Domain.Entities.Person> GetPersonByCpf(string cpf)
        {
            var response = await _client.GetAsync($"https://localhost:7048/api/person/{cpf}");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<UPBank.Person.Domain.Entities.Person>(result);
            }
            return null;
        }

        public Task<UPBank.Person.Domain.Entities.Person> PatchPerson(string cpf, Models.DTOs.PersonPatchDTO personPatchDTO)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(personPatchDTO), Encoding.UTF8, "application/json");
                var response = _client.PatchAsync($"https://localhost:7048/api/person/{cpf}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return Task.FromResult(JsonConvert.DeserializeObject<UPBank.Person.Domain.Entities.Person>(result));
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
