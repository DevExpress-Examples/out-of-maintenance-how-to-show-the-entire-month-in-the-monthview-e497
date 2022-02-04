using DevExpress.Office.Utils;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace T195089 {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Init(object sender, EventArgs e) {
            DateTime currentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            ASPxScheduler1.Start = currentMonth;
            ASPxScheduler1.MonthView.WeekCount = MonthViewHelper.CalculateWeekCount(ASPxScheduler1, currentMonth);
        }

        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void ASPxScheduler1_BeforeExecuteCallbackCommand(object sender, DevExpress.Web.ASPxScheduler.SchedulerCallbackCommandEventArgs e) {
            var scheduler = sender as ASPxScheduler;
            if(scheduler.ActiveViewType == SchedulerViewType.Month) {
                if(e.CommandId == SchedulerCallbackCommandId.NavigateForward) {
                    e.Command = new CustomNavigateForwardCallbackCommand(sender as ASPxScheduler);
                }
                if(e.CommandId == SchedulerCallbackCommandId.NavigateBackward) {
                    e.Command = new CustomNavigateBackwardCallbackCommand(sender as ASPxScheduler);
                }
                if(e.CommandId == SchedulerCallbackCommandId.GotoToday) {
                    e.Command = new CustomGotoTodayCallbackCommand(sender as ASPxScheduler);
                }
                if(e.CommandId == SchedulerCallbackCommandId.GotoDateForm) {
                    e.Command = new CustomGotoDateDialogCommand(sender as ASPxScheduler);
                }
                if(e.CommandId == SchedulerCallbackCommandId.GotoDate) {
                    e.Command = new CustomGotoDateCallbackCommand(sender as ASPxScheduler);
                }
                if(e.CommandId == SchedulerCallbackCommandId.OffsetVisibleIntervals) {
                    e.Command = new CustomOffsetVisibleIntervalsCallbackCommand(sender as ASPxScheduler);
                }
            }
            if(e.CommandId == SchedulerCallbackCommandId.SwitchView) {
                e.Command = new CustomSwitchViewCallbackCommand(sender as ASPxScheduler);
            }
        }
    }
}