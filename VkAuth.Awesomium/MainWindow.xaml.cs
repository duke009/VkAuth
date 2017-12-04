using System;
using System.Windows;
using Awesomium.Core;

namespace VkAuth.Awesomium
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IBrowser
    {
        public MainWindow()
        {
            InitializeComponent();

            browser.WebSession = WebCore.CreateWebSession(@"WebSession\", new WebPreferences { AcceptLanguage = "ru-ru,ru" });
            browser.AddressChanged += webBrowser_AddressChanged;
        }

        private void webBrowser_AddressChanged(object sender, UrlEventArgs e)
        {
            OnNavigated?.Invoke(e.Url);
        }

        public void Navigate(Uri uri)
        {
            browser.Source = uri;
        }

        public void BrowserOnNavigated(Action<Uri> callback)
        {
            OnNavigated += callback;
        }

        public Action<Uri> OnNavigated { get; set; }
    }
}
