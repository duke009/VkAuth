using System;
using System.Collections.Specialized;

namespace VkAuth
{
    public class VkAuthHelper
    {
        public static Uri BuildNavigateLink(Request request)
        {
            var uri = new Uri("http://oauth.vk.com/authorize")
                .AddQuery("client_id", request.ClientID.ToString())
                .AddQuery("display", request.Display.ToString().ToLower())
                .AddQuery("redirect_uri", request.RedirectUri.AbsoluteUri)
                .AddQuery("scope", request.Scope.BuildScope().ToString())
                .AddQuery("response_type", "token")
                .AddQuery("v", request.ApiVersion)
                .AddQuery("state", request.State);

            if (request.Revoke)
                uri = uri.AddQuery("revoke", "1");

            return uri;
        }


        public static Response GetResponse(Uri uri)
        {
            var queryDictionary = ParseUri(uri);

            return new Response
            {
                AccessToken = queryDictionary["access_token"],
                ExpiresIn = ParseInt(queryDictionary["expires_in"]),
                UserID = ParseInt(queryDictionary["user_id"]),
                State = queryDictionary["state"],
                HasError = !string.IsNullOrEmpty(queryDictionary["error"]),
                Error = queryDictionary["error"],
                ErrorDescription = queryDictionary["error_description"],
            };
        }

        public static NameValueCollection ParseUri(Uri uri)
        {
            var queryString = uri.Fragment.Replace("#", "");
            return System.Web.HttpUtility.ParseQueryString(queryString);
        }

        private static int? ParseInt(string str)
        {
            if(int.TryParse(str, out var value))
                return value;
            return null;
        }
    }
}