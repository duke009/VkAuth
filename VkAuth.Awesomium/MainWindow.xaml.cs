using System;
using System.Windows;
using Awesomium.Core;

namespace VkAuth.Awesomium
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window //, IVkAuth
    {
        public MainWindow()
        {
            InitializeComponent();

            //browser.WebSession = WebCore.CreateWebSession(@"WebSession\", new WebPreferences { AcceptLanguage = "ru-ru,ru" });
            //browser.AddressChanged += webBrowser_AddressChanged;
        }

        //private void webBrowser_AddressChanged(object sender, UrlEventArgs e)
        //{
        //    var uri = e.Url;
        //    if (!uri.AbsolutePath.Contains(RedirectUri.AbsolutePath))
        //        return;

        //    OnResponse?.Invoke(VkAuthHelper.GetResponse(uri));
        //}


        //public void Login(Request request)
        //{
        //    RedirectUri = request.RedirectUri;
        //    browser.Source = VkAuthHelper.BuildNavigateLink(request);
        //}

        //public Uri RedirectUri { get; private set; }
        //public Action<Response> OnResponse { get; set; }
    }
}
