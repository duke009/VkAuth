using System;

namespace VkAuth
{
    public class VkAuthHelper
    {
        public static Uri BuildNavigateLink(Request request)
        {
            var revokeStr = request.Revoke ? "&revoke=1" : "";
            return new Uri($"oauth.vk.com/authorize?client_id={request.ClientID}&display={request.Display}&redirect_uri={request.RedirectUri.AbsolutePath}&scope={request.Scope.BuildScope()}&response_type=token&v={request.ApiVersion}&state={request.State}{revokeStr}");
        }

        public static Response GetResponse(Uri uri)
        {
            var queryString = uri.Query;
            var queryDictionary = System.Web.HttpUtility.ParseQueryString(queryString);

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

        private static int? ParseInt(string str)
        {
            if(int.TryParse(str, out var value))
                return value;
            return null;
        }
    }
}