using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UPBank.Address.Domain.Entities
{
    public class Address
    {
        [Key]
        [JsonProperty("cep")]
        public string ZipCode { get; set; }

        [JsonProperty("logradouro")]
        public string Street { get; set; }

        [JsonProperty("localidade")]
        public string City { get; set; }

        [JsonProperty("uf")]
        public string State { get; set; }

        [JsonProperty("bairro")]
        public string Neighborhood { get; set; }

        public void FormatZipCode()
        {
            ZipCode = ZipCode.Replace("-", "");
        }
    }
}