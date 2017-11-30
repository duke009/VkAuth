namespace VkAuth.WinForm
{
    public class Factory : IFactory
    {
        public IVkAuth Create()
        {
            IBrowser browser = null;
            var tread = new STAThread(() =>
            {
                browser = new MainWindow();
                ((MainWindow)browser).Show();
                return true;
            });

            return new VkAuthBrowser(browser, tread);
        }
    }
}
