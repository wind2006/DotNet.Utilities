using DevExpress.XtraEditors;
using System.ComponentModel;
using System.Windows.Forms;

namespace YanZhiwei.DotNet.DevExpress12._1.Utilities.CustomControl.CustomTrackBar
{
    public partial class TrackBarEx : TrackBarControl
    {
        public TrackBarEx()
            : base()
        {
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        static TrackBarEx()
        {
            RepositoryItemTrackBarEx.RegisterCustomTrackBar();
        }

        public override string EditorTypeName
        {
            get { return RepositoryItemTrackBarEx.CustomTrackBarName; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemTrackBarEx Properties
        {
            get
            {
                return base.Properties as RepositoryItemTrackBarEx;
            }
        }
    }
}