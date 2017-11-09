using System;

namespace VkAuth.Awesomium
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
                var program = new Programs();
                Api = (IVkAuth)program.MainForm;

                Api = program.Main();
                program.Run();
            });
            return Api;
        }
    }
}