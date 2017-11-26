using System;
using System.Windows.Forms;

namespace VkAuth.IE
{
    public partial class BrowserForm : Form, IBrowser
    {
        public BrowserForm()
        {
            InitializeComponent();
            browser.Navigated += BrowserOnNavigated;
        }

        private void BrowserOnNavigated(object sender, WebBrowserNavigatedEventArgs webBrowserNavigatedEventArgs)
        {
            OnNavigated?.Invoke(webBrowserNavigatedEventArgs.Url);
            Close();
        }

        public void Navigate(Uri uri)
        {
            browser.Navigate(uri.AbsoluteUri);
        }

        public void BrowserOnNavigated(Action<Uri> callback)
        {
            OnNavigated += callback;
        }

        public Action<Uri> OnNavigated { get; set; }
    }
}
