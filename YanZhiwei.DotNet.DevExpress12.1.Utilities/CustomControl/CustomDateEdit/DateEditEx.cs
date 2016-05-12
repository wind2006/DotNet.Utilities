using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Calendar;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Repository;
using System;

namespace YanZhiwei.DotNet.DevExpress12._1.Utilities.CustomControl.CustomDateEdit
{
    /// <summary>
    /// DateEdit 选择年月
    /// <para>参考：https://www.devexpress.com/Support/Center/Question/Details/CQ60337 </para>
    /// </summary>
    public class DateEditEx : DateEdit
    {
        public DateEditEx()
        {
            Properties.VistaDisplayMode = DefaultBoolean.True;
            Properties.DisplayFormat.FormatString = "yyyy-MM";
            Properties.DisplayFormat.FormatType = FormatType.DateTime;
            Properties.Mask.EditMask = "yyyy-MM";
            Properties.ShowToday = false;
        }

        protected override PopupBaseForm CreatePopupForm()
        {
            if (Properties.VistaDisplayMode == DefaultBoolean.True)
                return new CustomVistaPopupDateEditForm(this);
            return new PopupDateEditForm(this);
        }

        private DateResultModeEnum _dateMode = DateResultModeEnum.FirstDayOfMonth;

        public DateResultModeEnum DateMode
        {
            get { return _dateMode; }
            set { _dateMode = value; }
        }

        public enum DateResultModeEnum : int
        {
            FirstDayOfMonth = 1,
            LastDayOfMonth = 2
        }
    }

    public class CustomVistaPopupDateEditForm : VistaPopupDateEditForm
    {
        public CustomVistaPopupDateEditForm(DateEdit ownerEdit)
            : base(ownerEdit)
        {
        }

        protected override DateEditCalendar CreateCalendar()
        {
            return new CustomVistaDateEditCalendar(OwnerEdit.Properties, OwnerEdit.EditValue);
        }
    }

    public class CustomVistaDateEditCalendar : VistaDateEditCalendar
    {
        public CustomVistaDateEditCalendar(RepositoryItemDateEdit item, object editDate)
            : base(item, editDate)
        {
        }

        protected override void Init()
        {
            base.Init();
            this.View = DateEditCalendarViewType.YearInfo;
        }

        public DateEditEx.DateResultModeEnum DateMode
        {
            get
            {
                return ((DateEditEx)this.Properties.OwnerEdit).DateMode;
            }
        }

        protected override void OnItemClick(CalendarHitInfo hitInfo)
        {
            DayNumberCellInfo cell = hitInfo.HitObject as DayNumberCellInfo;
            if (View == DateEditCalendarViewType.YearInfo)
            {
                DateTime dt = new DateTime(DateTime.Year, cell.Date.Month, 1, 0, 0, 0);
                if (DateMode == DateEditEx.DateResultModeEnum.FirstDayOfMonth)
                {
                    OnDateTimeCommit(dt, false);
                }
                else
                {
                    DateTime tempDate = dt.AddMonths(1).AddDays(-1);
                    tempDate = new DateTime(tempDate.Year, tempDate.Month, tempDate.Day, 23, 59, 59);
                    OnDateTimeCommit(tempDate, false);
                }
            }
            else
            {
                base.OnItemClick(hitInfo);
            }
        }
    }
}