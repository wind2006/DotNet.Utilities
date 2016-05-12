using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using System;

namespace YanZhiwei.DotNet.DevExpress12._1.Utilities.CustomControl.CustomTrackBar
{
    [UserRepositoryItem("RegisterCustomTrackBar")]
    public partial class RepositoryItemTrackBarEx : RepositoryItemTrackBar
    {
        private static readonly object drawingTick = new object();
        private string tickDisplayText;

        // Static constructor should call registration method
        static RepositoryItemTrackBarEx()
        {
            RegisterCustomTrackBar();
        }

        public const string CustomTrackBarName = "CustomTrackBar";

        public override string EditorTypeName { get { return CustomTrackBarName; } }

        public static void RegisterCustomTrackBar()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(
                CustomTrackBarName, typeof(TrackBarEx), typeof(RepositoryItemTrackBarEx),
                typeof(CustomTrackBarViewInfo), new TrackBarPainter(), true));
        }

        public event EventHandler DrawingTick
        {
            add { Events.AddHandler(RepositoryItemTrackBarEx.drawingTick, value); }
            remove { Events.AddHandler(RepositoryItemTrackBarEx.drawingTick, value); }
        }

        protected internal virtual void OnDrawingTick(EventArgs e)
        {
            EventHandler handler = (EventHandler)Events[RepositoryItemTrackBarEx.drawingTick];
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
            RepositoryItemTrackBarEx source = item as RepositoryItemTrackBarEx;
            if (source == null) return;
            this.TickDisplayText = source.TickDisplayText;
            EndUpdate();
            Events.AddHandler(RepositoryItemTrackBarEx.drawingTick, source.Events[RepositoryItemTrackBarEx.drawingTick]);
        }
    }
}