using System;
using System.Windows;
using System.Windows.Navigation;

namespace VkAuth.IE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IBrowser
    {
        public MainWindow()
        {
            InitializeComponent();
            browser.Navigated += BrowserOnNavigated;
        }

        private void BrowserOnNavigated(object sender, NavigationEventArgs navigationEventArgs)
        {
            OnNavigated?.Invoke(navigationEventArgs.Uri);
        }

        public Action<Uri> OnNavigated { get; set; }

        public void Navigate(Uri uri)
        {
            browser.Navigate(uri);
        }

        public void BrowserOnNavigated(Action<Uri> callback)
        {
            OnNavigated += callback;
        }
    }
}
