using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class Country
    {
        // private fields
        private List<Province> providences = new List<Province>();
        
        // public properties
        public int CountryID { get; set; }
        public String CountryName { get; set; }
        public List<Province> Providences { get { return providences; } }

        // methods
        public void AddProvidence(Province prov) => providences.Add(prov);
        public Province RemoveProvidence(Province prov)
        {
            Province removedProv = null;
            foreach(Province p in providences)
            {
                if(p.ProvinceID == prov.ProvinceID)
                {
                    removedProv = p;
                    providences.Remove(prov);
                    return p;
                }
            }
            return removedProv;
        }
    }
}
