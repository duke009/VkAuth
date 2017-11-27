namespace VkAuth.Awesomium
{
    public class Factory : IFactory
    {
        public IVkAuth Create()
        {
            IVkAuth Api = null;
            var tread = new STAThread(() =>
            {
                var program = new MainWindow();
                Api = (IVkAuth) program;
                return false;
            });
            return Api;
        }
    }
}
