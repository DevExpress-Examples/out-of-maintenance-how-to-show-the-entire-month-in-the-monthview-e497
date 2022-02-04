
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;

public class MonthViewHelper {
    public static int CalculateWeekCount(ASPxScheduler scheduler, DateTime monthStart) {
        DayOfWeek localDoW = scheduler.OptionsView.FirstDayOfWeek == DevExpress.XtraScheduler.FirstDayOfWeek.System ? Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek :
            (DayOfWeek)scheduler.OptionsView.FirstDayOfWeek;

        DateTime weekStart = FirstDayOfWeek(monthStart, localDoW);
        DateTime monthEnd = monthStart.AddMonths(1);
        int weekCount = 0;
        while(weekStart < monthEnd) {
            weekCount++;
            weekStart = weekStart.AddDays(7);
        }
        return weekCount;
    }

    public static DateTime FirstDayOfWeek(DateTime date, DayOfWeek startOfWeek) {
        int diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
        return date.AddDays(-1 * diff).Date;
    }
}

public class CustomOffsetVisibleIntervalsCallbackCommand : OffsetVisibleIntervalsCallbackCommand {
    public CustomOffsetVisibleIntervalsCallbackCommand(ASPxScheduler scheduler) : base(scheduler) { }

    protected override void ExecuteCore() {
        //base.ExecuteCore();
        DateTime currentMonth = FirstDate.AddDays(15);
        currentMonth = new DateTime(currentMonth.Year, currentMonth.Month, 1);
        Control.Start = currentMonth;
        Control.MonthView.WeekCount = MonthViewHelper.CalculateWeekCount(Control, currentMonth);
    }
}
public class CustomNavigateForwardCallbackCommand : NavigateForwardCallbackCommand {
    public CustomNavigateForwardCallbackCommand(ASPxScheduler scheduler) : base(scheduler) { }

    protected override void ExecuteCore() {
        //base.ExecuteCore();
        DateTime currentMonth = Control.Start.AddDays(15);
        currentMonth = new DateTime(currentMonth.Year, currentMonth.Month, 1);
        DateTime newStartDate = currentMonth.AddMonths(1);
        Control.Start = newStartDate;
        Control.MonthView.WeekCount = MonthViewHelper.CalculateWeekCount(Control, newStartDate);
    }
}

public class CustomNavigateBackwardCallbackCommand : NavigateBackwardCallbackCommand {
    public CustomNavigateBackwardCallbackCommand(ASPxScheduler scheduler) : base(scheduler) { }

    protected override void ExecuteCore() {
        //base.ExecuteCore();
        DateTime currentMonth = Control.Start.AddDays(15);
        currentMonth = new DateTime(currentMonth.Year, currentMonth.Month, 1);
        DateTime newStartDate = currentMonth.AddMonths(-1);
        Control.Start = newStartDate;
        Control.MonthView.WeekCount = MonthViewHelper.CalculateWeekCount(Control, newStartDate);
    }
}

public class CustomGotoTodayCallbackCommand : GotoTodayCallbackCommand {
    public CustomGotoTodayCallbackCommand(ASPxScheduler scheduler) : base(scheduler) { }

    protected override void ExecuteCore() {
        //base.ExecuteCore();
        DateTime newStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        Control.Start = newStartDate;
        Control.MonthView.WeekCount = MonthViewHelper.CalculateWeekCount(Control, newStartDate);
    }
}

public class CustomGotoDateDialogCommand : DevExpress.Web.ASPxScheduler.Internal.GotoDateFormCallbackCommand {
    public CustomGotoDateDialogCommand(ASPxScheduler scheduler) : base(scheduler) { }

    protected override void ExecuteCore() {
        DateTime newStartDate = new DateTime(NewDate.Date.Year, NewDate.Date.Month, 1);
        Control.Start = newStartDate;
        Control.MonthView.WeekCount = MonthViewHelper.CalculateWeekCount(Control, newStartDate);
    }
}

public class CustomGotoDateCallbackCommand : GotoDateCallbackCommand {
    public CustomGotoDateCallbackCommand(ASPxScheduler scheduler) : base(scheduler) { }

    protected override void ExecuteCore() {
        //base.ExecuteCore();
        DateTime newStartDate = new DateTime(NewDate.Year, NewDate.Month, 1);
        Control.Start = newStartDate;
        Control.MonthView.WeekCount = MonthViewHelper.CalculateWeekCount(Control, newStartDate);
    }
}

public class CustomSwitchViewCallbackCommand : SwitchViewCallbackCommand {
    public CustomSwitchViewCallbackCommand(ASPxScheduler scheduler) : base(scheduler) { }

    protected override void ExecuteCore() {
        base.ExecuteCore();
        if(Control.ActiveViewType == SchedulerViewType.Month) {
            DateTime newStartDate = new DateTime(Control.Start.Year, Control.Start.Month, 1);
            Control.Start = newStartDate;
            Control.MonthView.WeekCount = MonthViewHelper.CalculateWeekCount(Control, newStartDate);
        }
    }
}