using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dropShippingApp.APIModels
{
    public class ImgurTokenResponse
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string expires_in { get; set; }
        public string token_type { get; set; }
        public string account_username { get; set; }
    }
}
