using System;
using System.Windows.Forms;
using Gecko;

namespace VkAuth.GeckoFx
{
    internal class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        internal IBrowser Main()
        {
            Xpcom.Initialize("Firefox");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var form = new Form1();
            Application.Run(form);
            return form;
        }
    }
}
