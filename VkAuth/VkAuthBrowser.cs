using System;

namespace VkAuth
{
    public class VkAuthBrowser : IVkAuth
    {
        public Uri RedirectUri { get; private set; }

        private IBrowser Browser { get; }

        public VkAuthBrowser(IBrowser browser)
        {
            Browser = browser;
            Browser.BrowserOnNavigated(BrowserOnNavigated);
        }

        public void Login(Request request)
        {
            RedirectUri = request.RedirectUri;
            Browser.Navigate(VkAuthHelper.BuildNavigateLink(request));
        }

        public void BrowserOnNavigated(Uri uri)
        {
            if (!uri.AbsolutePath.Contains(RedirectUri.AbsolutePath))
                return;

            OnResponse?.Invoke(VkAuthHelper.GetResponse(uri));
        }

        public Action<Response> OnResponse { get; set; }
    }
}