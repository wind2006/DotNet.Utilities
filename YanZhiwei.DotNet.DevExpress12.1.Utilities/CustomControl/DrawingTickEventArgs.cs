using DevExpress.XtraEditors.Drawing;
using System;

namespace YanZhiwei.DotNet.DevExpress12._1.Utilities.CustomControl
{
    public class DrawingTickEventArgs : EventArgs
    {
        private TrackBarObjectInfoArgs trackBarObject;
        private int tickCount;

        public DrawingTickEventArgs(TrackBarObjectInfoArgs _trackBarObject, int _tickCount)
        {
            trackBarObject = _trackBarObject;
            tickCount = _tickCount;
        }

        public TrackBarObjectInfoArgs TrackBarObject { get { return trackBarObject; } }

        public int TickCount { get { return tickCount; } }
    }
}