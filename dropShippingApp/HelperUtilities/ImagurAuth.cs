using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace dropShippingApp.HelperUtilities
{
    public class ImagurAuth
    {
        public static IRestResponse GetAccessToken(string clientId, string clientSecret, string refreshToken)
        {
            var client = new RestClient("https://api.imgur.com/oauth2/token");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("refresh_token", refreshToken);
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);
            request.AddParameter("grant_type", "refresh_token");
            IRestResponse response = client.Execute(request);
            return response;
        }
    }
}
