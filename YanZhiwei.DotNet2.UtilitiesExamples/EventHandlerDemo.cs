using System;
using System.Threading;
using YanZhiwei.DotNet2.Utilities.Common;
namespace YanZhiwei.DotNet2.UtilitiesExamples
{
    internal class EventHandlerDemo
    {
        //private static void Main(string[] args)
        //{
        //    try
        //    {
        //        trap t = new trap();
        //        machine machineA = new machine();
        //        t.TrapOccurred += machineA.c_TrapOccurred; //notify machine A
        //        DeMO tt = new DeMO();
        //        tt.TrapOccurred += Tt_TrapOccurred;
        //        t.run();
        //        tt.run();

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        Console.ReadLine();
        //    }
        //}

        private static void Tt_TrapOccurred(object sender, EventArgs e)
        {
            Console.WriteLine("<Alert>:  time/{0}", DateTime.Now.ToString());
        }

        public class machine
        {
            public void c_TrapOccurred(object sender, TrapInfoEventArgs e)
            {
                Console.WriteLine("<Alert>: cauese/{0}, info/ {1}, ip/{2}, time/{3}",
                    e.cause, e.info, e.ip, DateTime.Now.ToString());
            }
        }
    }
    public class DeMO
    {
        public event EventHandler TrapOccurred;
        protected virtual void OnTrapOccurred()
        {
            EventHandler handler = TrapOccurred;
            handler.RaiseEvent(this);
        }
        public void run()
        {
            OnTrapOccurred();
        }
    }
    public class trap
    {
        public event EventHandler<TrapInfoEventArgs> TrapOccurred;

        protected virtual void OnTrapOccurred(TrapInfoEventArgs e)
        {
            EventHandler<TrapInfoEventArgs> handler = TrapOccurred;
            handler.RaiseEvent(this, e);
            //if (handler != null)
            //{
            //    handler(this, e); 
            //}
        }

        public void run()
        {
            Thread.Sleep(500);
            TrapInfoEventArgs args = new TrapInfoEventArgs();
            args.cause = "Shut Down";

            OnTrapOccurred(args);
        }
    }

    public class TrapInfoEventArgs : EventArgs
    {
        public int info { get; set; }
        public string ip { get; set; }
        public string cause { get; set; }
    }
}