using System;
using System.Windows.Forms;

namespace VkAuth.Awesomium
{
    public class Programs
    {
        public Form MainForm { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public IVkAuth Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new Form1();
            return (IVkAuth)MainForm;
        }
        [STAThread]
        public void Run()
        {
            Application.Run(MainForm);
        }
    }
}
