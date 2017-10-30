using System;
using System.Collections.Generic;

namespace VkAuth
{
    public interface IVkAuth
    {
       void Login(Request request);
       Action<Response> OnResponse { get; }
    }

    public enum Scope
    {
        Notify = 1 << 0,
        Friends = 1 << 1,
        Photos = 1 << 2,
        Audio = 1 << 3,
        Video = 1 << 4,
        Pages = 1 << 5,
        Link = 1 << 6,
        Status = 1 << 7,
        Notes = 1 << 8,
        Messages = 1 << 9,
        Wall = 1 << 10,
        Ads = 1 << 11,
        Offline = 1 << 12,
        Docs = 1 << 13,
        Groups = 1 << 14,
        Notifications = 1 << 15,
        Stats = 1 << 16,
        Email = 1 << 17,
        Market = 1 << 18,
    }

    public enum DisplayPageType
    {
        Page,
        Popup,
        Mobile
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
