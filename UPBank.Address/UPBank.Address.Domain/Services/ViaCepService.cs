using Newtonsoft.Json;
using UPBank.Address.Domain.Contracts;

namespace UPBank.Address.Domain.Services
{
    public class ViaCepService : IViaCepService
    {
        public async Task<Entities.Address> GetAddressByZipCode(string zipCode)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://viacep.com.br/");
                var response = await client.GetAsync($"ws/{zipCode}/json/");
                if (response.IsSuccessStatusCode)
                {
                    var address = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Domain.Entities.Address>(address);
                }
                return null;
            }
        }
    }
}