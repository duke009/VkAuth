namespace VkAuth.GeckoFx
{
    public class Factory : IFactory
    {
        public IVkAuth Create()
        {
            IVkAuth Api = null;
            var tread = new STAThread(() =>
            {
                var program = new Program();
                
                var browser = program.Main();
                Api = new VkAuthBrowser(browser);
            });
            return Api;
        }
    }
}