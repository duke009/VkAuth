using System;
using System.Windows.Forms;

namespace VkAuth.IE
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
