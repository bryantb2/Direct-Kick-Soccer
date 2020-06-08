using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.APIModels
{
    public class ImgurSubDataResponse
    {
        public string id { get; set; }
        public string title { get; set; }
        public string datetime { get; set; }
        public string type { get; set; }
        public string animated { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int size { get; set; }
        public int views { get; set; }
        public int bandwidth { get; set; }
        public string vote { get; set; }
        public string favorite { get; set; }
        public string nsfw { get; set; }
        public string section { get; set; }
        public string account_url { get; set; }
        public int account_id { get; set; }
        public string is_ad { get; set; }
        public string in_most_viral { get; set; }
        public string[] tags { get; set; }
        public int ad_type { get; set; }
        public string ad_url { get; set; }
        public string in_gallery { get; set; }
        public string deletehash { get; set; }
        public string name { get; set; }
        public string link { get; set; }
    }
}
