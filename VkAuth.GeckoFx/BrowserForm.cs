using System;
using System.Windows.Forms;
using Gecko;

namespace VkAuth.GeckoFx
{
    public partial class BrowserForm : Form, IBrowser
    {
        public BrowserForm()
        {
            InitializeComponent();
            browser.Navigated += BrowserOnNavigated;
        }

        private void BrowserOnNavigated(object sender, GeckoNavigatedEventArgs geckoNavigatedEventArgs)
        {
            OnNavigated?.Invoke(geckoNavigatedEventArgs.Uri);
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