using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

namespace YanZhiwei.DotNet.DevExpress12._1.Utilities.CustomControl.CustomRangeTrackBar
{
    public class CustomRangeTrackBarViewInfo : RangeTrackBarViewInfo
    {
        public CustomRangeTrackBarViewInfo(RepositoryItem item)
            : base(item)
        {
        }

        public RepositoryItemRangeTrackBarEx RepositoryItem
        {
            get { return this.Item as RepositoryItemRangeTrackBarEx; }
        }

        public override DevExpress.XtraEditors.Drawing.TrackBarObjectPainter GetTrackPainter()
        {
            //if (IsPrinting)
            //    return new RangeTrackBarObjectPainter();
            //if (this.LookAndFeel.ActiveStyle == ActiveLookAndFeelStyle.WindowsXP)
            //    return new RangeTrackBarObjectPainter();
            //if (this.LookAndFeel.ActiveStyle == ActiveLookAndFeelStyle.Skin)
            return new SkinCustomRangeTrackBarObjectPainter(LookAndFeel.ActiveLookAndFeel);
            //if (this.LookAndFeel.ActiveStyle == ActiveLookAndFeelStyle.Office2003)
            //    return new Office2003RangeTrackBarObjectPainter();
            //return new RangeTrackBarObjectPainter();
        }
    }
}