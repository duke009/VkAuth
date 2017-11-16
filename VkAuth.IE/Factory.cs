namespace VkAuth.IE
{
    public class Factory : IFactory
    {
        //var s = tread.Invoke((System.Threading.ThreadStart) delegate()
        //{

        //});
        public IVkAuth Create()
        {
            IVkAuth Api = null;
            var tread = new STAThread(() =>
            {
                var browser = new MainWindow();
                Api = new VkAuthBrowser(browser);
                return true;
            });
            return Api;
        }
    }
}
