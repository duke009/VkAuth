using System;
using System.Threading.Tasks;

namespace VkAuth
{
    public class VkAuthBrowser : IVkAuth
    {
        private STAThread Thread { get; }

        public Uri RedirectUri { get; private set; }

        private IBrowser Browser { get; }

        public VkAuthBrowser(IBrowser browser, STAThread thread = null)
        {
            Thread = thread;
            Browser = browser;
            Browser.BrowserOnNavigated(BrowserOnNavigated);
        }

        public void Login(Request request)
        {
            RedirectUri = request.RedirectUri;
            Thread.BeginInvoke((Action)(() => { Browser.Navigate(VkAuthHelper.BuildNavigateLink(request)); }));
        }

        private void BrowserOnNavigated(Uri uri)
        {
            if (!uri.AbsolutePath.Contains(RedirectUri.AbsolutePath))
                return;

            Task.Factory.StartNew(() => OnResponse?.Invoke(VkAuthHelper.GetResponse(uri)));
            Thread.Dispose();
        }

        public Action<Response> OnResponse { get; set; }
    }
}