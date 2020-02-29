using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class Country
    {
        // private fields
        private List<Providence> providences = new List<Providence>();
        
        // public properties
        public int CountryID { get; set; }
        public String CountryName { get; set; }
        public List<Providence> Providences { get { return providences; } }

        // methods
        public void AddProvidence(Providence prov) => providences.Add(prov);
        public Providence RemoveProvidence(Providence prov)
        {
            Providence removedProv = null;
            foreach(Providence p in providences)
            {
                if(p.ProvidenceID == prov.ProvidenceID)
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
