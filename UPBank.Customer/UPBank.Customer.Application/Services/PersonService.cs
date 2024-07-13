using Newtonsoft.Json;
using System.Text;
using UPBank.Utils.Person.Contracts;

namespace UPBank.Utils.Person.Services
{
    public class PersonService : IPersonService
    {
        private static readonly HttpClient _client = new HttpClient();
        public async Task<bool> CreatePerson(UPBank.Person.Domain.Entities.Person person)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("https://localhost:7048/api/peoples", content);
                if (response.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CheckIfExist(string cpf)
        {
            try
            {
                var response = await _client.GetAsync($"https://localhost:7048/api/peoples/{cpf}");
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
            var response = await _client.GetAsync($"https://localhost:7048/api/peoples/{cpf}");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<UPBank.Person.Domain.Entities.Person>(result);
            }
            return null;
        }

        public async Task<UPBank.Person.Domain.Entities.Person> PatchPerson(string cpf, Models.DTOs.PersonPatchDTO personPatchDTO)
        {
            var content = new StringContent(JsonConvert.SerializeObject(personPatchDTO), Encoding.UTF8, "application/json");
            var response = await _client.PatchAsync($"https://localhost:7048/api/peoples/{cpf}", content);
            
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<UPBank.Person.Domain.Entities.Person>(result);
            }
            return null;
        }
    }
}
