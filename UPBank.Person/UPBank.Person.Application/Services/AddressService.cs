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
        public async Task<AddressOutputModel> CreateAddress(AddressInputModel addressInputModel)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(addressInputModel), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("https://localhost:7082/api/addresses", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<AddressOutputModel>(result);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error on create address");
            }
            return null;
        }

        public async Task<AddressOutputModel> GetCompleteAddressById(Guid id)
        {
            try
            {
                var response = await _client.GetAsync($"https://localhost:7082/api/addresses/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<AddressOutputModel>(result);
                }
                return null;
            }
            catch (Exception)
            {
                throw new Exception("Error on get complete address by id");
            }
        }

        public async Task<CompleteAddress> UpdateAddress(Guid id, AddressInputModel addressInputModel)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(addressInputModel), Encoding.UTF8, "application/json");
                var response = await _client.PatchAsync($"https://localhost:7082/api/addresses/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<CompleteAddress>(result);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error on update address");
            }
            return null;
        }




        //public async Task<Models.Address> GetAddressByZipCode(string zipCode)
        //{
        //    try
        //    {
        //        var response = _client.GetAsync($"https://localhost:7148/api/addresses/{zipCode}");

        //        if (response.Result.IsSuccessStatusCode)
        //        {
        //            var result = await response.Result.Content.ReadAsStringAsync(); 
        //            return JsonConvert.DeserializeObject<Models.Address>(result);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("Error on get address by zip code");
        //    }
        //    return null;
        //}


    }
}
