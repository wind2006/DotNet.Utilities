using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

namespace YanZhiwei.DotNet.DevExpress12._1.Utilities.CustomControl.CustomTrackBar
{
    public class CustomTrackBarViewInfo : TrackBarViewInfo
    {
        public CustomTrackBarViewInfo(RepositoryItem item)
            : base(item)
        {
        }

        public RepositoryItemTrackBarEx RepositoryItem
        { get { return this.Item as RepositoryItemTrackBarEx; } }

        public override TrackBarObjectPainter GetTrackPainter()
        {
            //if (IsPrinting)
            //    return new TrackBarObjectPainter();
            //if (this.LookAndFeel.ActiveStyle == ActiveLookAndFeelStyle.WindowsXP)
            //    return new WindowsXPTrackBarObjectPainter();
            //if (this.LookAndFeel.ActiveStyle == ActiveLookAndFeelStyle.Skin)
            return new SkinCustomTrackBarObjectPainter(LookAndFeel.ActiveLookAndFeel);
            //if (this.LookAndFeel.ActiveStyle == ActiveLookAndFeelStyle.Office2003)
            //    return new Office2003TrackBarObjectPainter();
            //return new TrackBarObjectPainter();
        }
    }
}