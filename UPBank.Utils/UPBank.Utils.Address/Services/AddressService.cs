using Newtonsoft.Json;
using System.Text;
using UPBank.Address.Application.Models;
using UPBank.Address.Domain.Entities;
using UPBank.Utils.Address.Contracts;

namespace UPBank.Utils.Address.Services
{
    public class AddressService : IAddressService
    {
        private static readonly HttpClient _client = new HttpClient();
        public async Task<(AddressOutputModel addressOutputModel, string message)> CreateAddress(AddressInputModel addressInputModel)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(addressInputModel), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("https://localhost:7082/api/addresses", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return (JsonConvert.DeserializeObject<AddressOutputModel>(result), null);
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (null, "Houve um erro ao criar endereço: " + errorMessage);
                }
            }
            catch (Exception e)
            {
                return (null, "Houve um erro ao criar endereço: " + e);
            }
        }

        public async Task<(AddressOutputModel addressOutputModel, string message)> GetCompleteAddressById(Guid id)
        {
            try
            {
                var response = await _client.GetAsync($"https://localhost:7082/api/addresses/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return (JsonConvert.DeserializeObject<AddressOutputModel>(result), null);
                }

                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (null, "Houve um erro ao trazer o endereço: " + errorMessage);
                }
            }
            catch (Exception e)
            {
                return (null, "Houve um erro ao trazer o endereço: " + e);               
            }
        }

        public async Task<(CompleteAddress completeAddress, string message)> UpdateAddress(Guid id, AddressInputModel addressInputModel)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(addressInputModel), Encoding.UTF8, "application/json");
                var response = await _client.PatchAsync($"https://localhost:7082/api/addresses/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return (JsonConvert.DeserializeObject<CompleteAddress>(result), null);
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (null, "Houve um erro ao atualizar o endereço: " + errorMessage);
                }
            }
            catch (Exception e)
            {
                return (null, "Houve um erro ao atualizar o endereço:" + e);             
            }
        }
    }
}
