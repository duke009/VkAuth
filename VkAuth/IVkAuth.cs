using System;
using System.Collections.Generic;
using VkAuth.Enums;

namespace VkAuth
{
    public interface IVkAuth
    {
       void Login(Request request);
       Action<Response> OnResponse { get; set; }
    }

    public class Response
    {
        public string AccessToken { get; set; }
        public int? ExpiresIn { get; set; }
        public int? UserID { get; set; }
        public string State { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }
        public string ErrorDescription { get; set; }
    }

    public class Request
    {
        public string ClientID { get; set; }
        public Uri RedirectUri { get; set; }
        public HashSet<Scope> Scope { get; set; }
        public DisplayPageType Display { get; set; }
        public string State { get; set; }
        public string ApiVersion { get; set; } = "5.68";
        public bool Revoke { get; set; }
    }
}
