using Newtonsoft.Json;
using UPBank.Address.Application.Contracts;

namespace UPBank.Address.Application.Services
{
    public class ViaCepService : IViaCepService
    {
        public async Task<Domain.Entities.Address> GetAddressInAPI(string zipCode)
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
