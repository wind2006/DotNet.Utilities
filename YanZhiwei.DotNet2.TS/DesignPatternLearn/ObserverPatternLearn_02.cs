using System;

namespace YanZhiwei.DotNet2.TS.DesignPatternLearn
{
    public class DistanceArgs : EventArgs
    {
        public int NewDistance { get; set; }

        public DistanceArgs(int newDistance)
        {
            this.NewDistance = newDistance;
        }
    }

    public class PetTracker2
    {
        public string PetName { get; set; }

        public delegate void TrackerHanlder(object sender, DistanceArgs args);

        public event TrackerHanlder TrackEvent = delegate { };

        protected void OnTrackEvent(DistanceArgs args)
        {
            if (TrackEvent != null)
            {
                TrackEvent(this, args);
            }
        }

        private int curDistance;

        public int CurDistance
        {
            get
            {
                return curDistance;
            }
            set
            {
                if (value != curDistance)
                {
                    curDistance = value;
                    if (TrackEvent != null)
                    {
                        OnTrackEvent(new DistanceArgs(curDistance));
                    }
                }
            }
        }
    }

    public class MakerAlertSupplier2
    {
        public int Distance { get; set; }

        public MakerAlertSupplier2(int distance)
        {
            this.Distance = distance;
        }

        public void MakeAlert(object sender, DistanceArgs args)
        {
            if (args.NewDistance >= Distance)
            {
                PetTracker2 _petTracker = sender as PetTracker2;
                Console.WriteLine("嘀嘀嘀，您的宝贝『" + _petTracker.PetName + "』已经离你" + args.NewDistance + "米之外了，要注意哦~~");
            }
        }
    }

    public class ShowAlertSupplier2
    {
        public int Distance { get; set; }

        public ShowAlertSupplier2(int distance)
        {
            this.Distance = distance;
        }

        public void ShowAlert(object sender, DistanceArgs args)
        {
            if (args.NewDistance >= Distance)
            {
                PetTracker2 _petTracker = sender as PetTracker2;
                Console.WriteLine("您的宝贝『" + _petTracker.PetName + "』已经离你" + args.NewDistance + "米之外了，要注意哦~~");
            }
        }
    }
}