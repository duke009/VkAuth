
namespace VkAuth.IE
{
    public class Factory : IFactory
    {
        public IVkAuth Create()
        {
            IVkAuth Api = null;
            var tread = new STAThread((thread) =>
            {
                var program = new Program();
                
                program.Initialize();
                Api = new VkAuthBrowser(program.BrowserForm, thread);
                program.Run();
                return false;
            });
            return Api;
        }
    }
}