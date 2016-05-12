using System;

namespace YanZhiwei.DotNet2.TS.DesignPatternLearn
{
    /// <summary>
    /// 宠物追踪器
    /// </summary>
    //public class PetTracker
    //{
    //    /*
    //     * 观察者模式(Observer Pattern)有时又被称为订阅——发布模式，它主要应对这样的场景:需要将单一事件的通知(比如对象状态发生变化)广播给多个订阅者(观察者)。
    //     */

    //    private int distance;
    //    //适时监控
    //    public void InstanceTrack()
    //    {
    //        for (int i = 0; i < 102; i++)
    //        {
    //            distance = i;
    //            if (distance > 100)
    //            {
    //                MakeAlert(distance);
    //                ShowAlert(distance);
    //            }
    //        }
    //    }
    //    private void MakeAlert(int param)
    //    {
    //        Console.WriteLine("嘀嘀嘀，您的宝贝已经离你" + param + "米之外了，要注意哦~~");
    //    }
    //    public void ShowAlert(int param)
    //    {
    //        Console.WriteLine("您的宝贝已经离你" + param + "米之外了，要注意哦~~");
    //    }
    //}
    public class PetTracker
    {
        private int distance;

        public delegate void TrackHandler(int param);

        public event TrackHandler TrackEvent;

        public void InstanceTrack()
        {
            if (TrackEvent != null)
            {
                for (int i = 0; i < 102; i++)
                {
                    distance = i;
                    if (distance >= 100)
                    {
                        TrackEvent(distance);
                        break;
                    }
                }
            }
        }
    }

    public class MakerAlertSupplier
    {
        public void MakeAlert(int param)
        {
            Console.WriteLine("嘀嘀嘀，您的宝贝已经离你" + param + "米之外了，要注意哦~~");
        }
    }

    public class ShowAlertSupplier
    {
        public void ShowAlert(int param)
        {
            Console.WriteLine("您的宝贝已经离你" + param + "米之外了，要注意哦~~");
        }
    }
}