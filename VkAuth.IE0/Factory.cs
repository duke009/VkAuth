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
            var tread = new STAThread((thread) =>
            {
                var browser = new MainWindow();
                Api = new VkAuthBrowser(browser, thread);
                return true;
            });

            return Api;
        }
    }
}
