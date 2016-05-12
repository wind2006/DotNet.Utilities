using DevExpress.XtraEditors;
using System.ComponentModel;
using System.Windows.Forms;

namespace YanZhiwei.DotNet.DevExpress12._1.Utilities.CustomControl.CustomRangeTrackBar
{
    public partial class RangeTrackBarEx : RangeTrackBarControl
    {
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        static RangeTrackBarEx()
        {
            RepositoryItemRangeTrackBarEx.RegisterCustomRangeTrackBar();
        }

        public RangeTrackBarEx()
            : base()
        {
        }

        public override string EditorTypeName
        {
            get { return RepositoryItemRangeTrackBarEx.CustomRangeTrackBarName; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemRangeTrackBarEx Properties
        {
            get
            {
                return base.Properties as RepositoryItemRangeTrackBarEx;
            }
        }
    }
}