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
                
                program.Initialize();
                Api = new VkAuthBrowser(program.BrowserForm);
                program.Run();
            });
            return Api;
        }
    }
}