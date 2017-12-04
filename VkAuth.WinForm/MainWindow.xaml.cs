using System;
using System.Windows;

namespace VkAuth.WinForm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IBrowser
    {
        private Browser Browser = new Browser();
        private Uri uri { get; set; }
        private Action<Uri> OnNavigated { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        public void Navigate(Uri uri)
        {
            this.uri = uri;
        }

        public void BrowserOnNavigated(Action<Uri> callback)
        {
            OnNavigated += callback;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (uri == null)
                return;
            if (string.IsNullOrEmpty(loginTb.Text) || string.IsNullOrEmpty(passwordTb.Password))
                return;

            try
            {
                var responseUri = Browser.Authorize(loginTb.Text, passwordTb.Password, uri);
                OnNavigated(responseUri);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
