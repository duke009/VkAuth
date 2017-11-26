namespace VkAuth.GeckoFx
{
    public class Factory : IFactory
    {
        public IVkAuth Create()
        {
            IBrowser browser = null;
            var tread = new STAThread(() =>
            {
                var program = new Program();
                program.Initialize();
                browser = program.BrowserForm;
                program.Run();
                return false;
            });
            return new VkAuthBrowser(browser, tread);
        }
    }
}