using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VkAuth.Awesomium
{
    public partial class Form1 : Form, IVkAuth
    {
        public Form1()
        {
            InitializeComponent();
            webBrowser.Navigated += BrowserOnNavigated;
        }

        private void BrowserOnNavigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            var uri = e.Url;
            if (!uri.AbsolutePath.Contains(RedirectUri.AbsolutePath))
                return;

            OnResponse?.Invoke(VkAuthHelper.GetResponse(uri));
        }


        public void Login(Request request)
        {
            RedirectUri = request.RedirectUri;
            webBrowser.Navigate(VkAuthHelper.BuildNavigateLink(request));
        }

        public Uri RedirectUri { get; private set; }
        public Action<Response> OnResponse { get; set; }
    }
}
