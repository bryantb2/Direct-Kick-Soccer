using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using dropShippingApp.Models;
using Newtonsoft.Json;

namespace dropShippingApp.Data
{
    public class JsonUtil
    {
        //used to create country and state seed data from json
        public static List<Country> DeserializeCountry()
        {
            using (StreamReader reader = new StreamReader("country.json"))
            {
                string json = reader.ReadToEnd();
                List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(json);
                return countries;
            }
        }
        public static List<Province>DeserializeProvinces()
        {
           using(StreamReader reader=new StreamReader("state.json"))
           {
                string json = reader.ReadToEnd();
                List<Province> provinces = JsonConvert.DeserializeObject<List<Province>>(json);
                return provinces;
           }
        }
    }
}
