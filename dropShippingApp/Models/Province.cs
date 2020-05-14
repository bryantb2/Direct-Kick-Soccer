using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class Province
    {
        public int ProvinceID { get; set; }
        [JsonProperty("name")]
        public String ProvinceName { get; set; }
        [JsonProperty("abbreviation")]
        public string ProvienceAbbreviation { get; set; }
    }
}
