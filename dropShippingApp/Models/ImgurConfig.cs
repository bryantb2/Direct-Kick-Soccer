using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.Models
{
    public class ImgurConfig
    {
        public int ImgurConfigID { get; set; }
        public string AccessToken { get; set; }
        public DateTime AccessLastUpdated { get; set; }
    }
}
