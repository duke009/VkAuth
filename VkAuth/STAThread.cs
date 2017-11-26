using System;
using System.Threading;
using System.Windows.Forms;
namespace VkAuth
{
    public class STAThread : IDisposable
    {
        public STAThread(Func<bool> initialize)
        {
            using (mre = new ManualResetEvent(false))
            {
                thread = new Thread(() =>
                {
                    Application.Idle += Initialize;
                    if (initialize())
                        Application.Run();
                }) {IsBackground = true};
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                mre.WaitOne();
            }
        }

        public void BeginInvoke(Delegate dlg, params Object[] args)
        {
            if (ctx == null) throw new ObjectDisposedException("STAThread");
            ctx.Post(_ => dlg.DynamicInvoke(args), null);
        }

        protected virtual void Initialize(object sender, EventArgs e)
        {
            ctx = SynchronizationContext.Current;
            mre.Set();
            Application.Idle -= Initialize;
        }

        public void Dispose()
        {
            if (ctx != null)
            {
                ctx.Send(_ => Application.ExitThread(), null);
                ctx = null;
            }
        }
        private Thread thread;
        private SynchronizationContext ctx;
        private ManualResetEvent mre;
    }
}