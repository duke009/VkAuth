using System;
using System.Windows;
using System.Windows.Navigation;

namespace VkAuth.IE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IVkAuth
    {
        public MainWindow()
        {
            InitializeComponent();
            browser.Navigated += BrowserOnNavigated;
        }

        private void BrowserOnNavigated(object sender, NavigationEventArgs navigationEventArgs)
        {
            var uri = navigationEventArgs.Uri;
            if (!uri.AbsolutePath.Contains(RedirectUri.AbsolutePath))
                return;

            OnResponse?.Invoke(VkAuthHelper.GetResponse(uri));
        }


        public void Login(Request request)
        {
            RedirectUri = request.RedirectUri;
            browser.Navigate(VkAuthHelper.BuildNavigateLink(request));
        }

        public Uri RedirectUri { get; private set; }
        public Action<Response> OnResponse { get; }
        Action<Response> IVkAuth.OnResponse { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
