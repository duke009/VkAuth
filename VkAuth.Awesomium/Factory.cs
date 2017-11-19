﻿namespace VkAuth.Awesomium
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
                var program = new MainWindow();
                Api = (IVkAuth) program;
                return false;
            });
            return Api;
        }
    }
}
