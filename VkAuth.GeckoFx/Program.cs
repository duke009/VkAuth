using System;
using System.Windows.Forms;
using Gecko;

namespace VkAuth.GeckoFx
{
    internal class Program
    {
        public BrowserForm BrowserForm { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        internal void Initialize()
        {
            Xpcom.Initialize("Firefox");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            BrowserForm = new BrowserForm();
        }

        internal void Run()
        {
            Application.Run(BrowserForm);
        }
    }
}
