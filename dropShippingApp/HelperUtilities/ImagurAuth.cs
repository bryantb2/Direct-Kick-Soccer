using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dropShippingApp.APIModels;
using Microsoft.AspNetCore.Http;
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

        public static IRestResponse AddImage(ImgurUploadRequest imageData, string clientId)
        {
            var client = new RestClient("https://api.imgur.com/3/image");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Client-ID " + clientId);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("image", imageData.Image);
            request.AddParameter("type", imageData.Type);
            request.AddParameter("title", imageData.Title);
            request.AddParameter("description", imageData.Description);
            IRestResponse response = client.Execute(request);
            return response;
        }

        public static IRestResponse DeleteImage(string photoId, string accessToken)
        {
            var client = new RestClient("https://api.imgur.com/3/image/" + photoId);
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Authorization", "Bearer " + accessToken);
            request.AlwaysMultipartFormData = true;
            IRestResponse response = client.Execute(request);
            return response;
        }

        public static IRestResponse GetImageById(string clientId, string photoId)
        {
            var client = new RestClient("https://api.imgur.com/3/image/" + photoId);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Client-ID " + clientId);
            request.AlwaysMultipartFormData = true;
            IRestResponse response = client.Execute(request);
            return response;
        }
    }
}
