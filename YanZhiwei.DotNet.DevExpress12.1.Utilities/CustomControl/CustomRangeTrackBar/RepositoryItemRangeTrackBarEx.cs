using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using System;

namespace YanZhiwei.DotNet.DevExpress12._1.Utilities.CustomControl.CustomRangeTrackBar
{
    //The attribute that points to the registration method
    [UserRepositoryItem("RegisterCustomRangeTrackBar")]
    public class RepositoryItemRangeTrackBarEx : RepositoryItemRangeTrackBar
    {
        private static readonly object drawingTick = new object();
        private string tickDisplayText;

        // Static constructor should call registration method
        static RepositoryItemRangeTrackBarEx()
        {
            RegisterCustomRangeTrackBar();
        }

        public const string CustomRangeTrackBarName = "CustomRangeTrackBar";

        public override string EditorTypeName { get { return CustomRangeTrackBarName; } }

        public static void RegisterCustomRangeTrackBar()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(
                CustomRangeTrackBarName, typeof(RangeTrackBarEx), typeof(RepositoryItemRangeTrackBarEx),
                typeof(CustomRangeTrackBarViewInfo), new RangeTrackBarPainter(), true));
        }

        public event EventHandler DrawingTick
        {
            add { Events.AddHandler(RepositoryItemRangeTrackBarEx.drawingTick, value); }
            remove { Events.AddHandler(RepositoryItemRangeTrackBarEx.drawingTick, value); }
        }

        protected internal virtual void OnDrawingTick(EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[RepositoryItemRangeTrackBarEx.drawingTick];
            if (handler != null) handler(GetEventSender(), e);
        }

        public string TickDisplayText
        {
            set
            {
                tickDisplayText = value;
                RaisePropertiesChanged(EventArgs.Empty);
            }
            get
            {
                return tickDisplayText;
            }
        }

        // Override the Assign method
        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            base.Assign(item);
            RepositoryItemRangeTrackBarEx source = item as RepositoryItemRangeTrackBarEx;
            if (source == null) return;
            this.TickDisplayText = source.TickDisplayText;
            EndUpdate();
            Events.AddHandler(RepositoryItemRangeTrackBarEx.drawingTick, source.Events[RepositoryItemRangeTrackBarEx.drawingTick]);
        }
    }
}