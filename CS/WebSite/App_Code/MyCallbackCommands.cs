using System;
using System.Data;
using System.Configuration;
using System.Web;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Commands;

public class MySwitchViewCallbackCommand : SwitchViewCallbackCommand {
    public MySwitchViewCallbackCommand(ASPxScheduler control)
        : base(control) {
    }
    protected override void ExecuteCore() {
        base.ExecuteCore();
        if (Control.ActiveViewType == SchedulerViewType.Month)
            Control.Start = DateTimeNavigationHelper.CalculateMonthFirstDate(Control.Start);
    }
}
public class MyNavigateForwardCallbackCommand : NavigateForwardCallbackCommand {
    public MyNavigateForwardCallbackCommand(ASPxScheduler control)
        : base(control) {
    }
    protected override SchedulerCommand CreateCommand() {
        return new MyNavigateViewForwardCommand(Control);
    }
}
public class MyNavigateBackwardCallbackCommand : NavigateBackwardCallbackCommand {
    public MyNavigateBackwardCallbackCommand(ASPxScheduler control)
        : base(control) {
    }
    protected override SchedulerCommand CreateCommand() {
        return new MyNavigateViewBackwardCommand(Control);
    }
}
public class MyNavigateViewForwardCommand : NavigateViewForwardCommand {
    public MyNavigateViewForwardCommand(ISchedulerCommandTarget target)
        : base(target) {
    }
    protected override void ExecuteCore() {
        InnerControl.Start = DateTimeNavigationHelper.CalculateNextMonthStartDate(InnerControl.Start);
    }
}
public class MyNavigateViewBackwardCommand : NavigateViewBackwardCommand {
    public MyNavigateViewBackwardCommand(ISchedulerCommandTarget target)
        : base(target) {
    }
    protected override void ExecuteCore() {
        InnerControl.Start = DateTimeNavigationHelper.CalculatePrevMonthStartDate(InnerControl.Start);
    }
}
public static class DateTimeNavigationHelper {
    public static DateTime CalculateNextMonthStartDate(DateTime startDate) {
        DateTime newDate = startDate.AddMonths(1);
        if (newDate.Day > 22)
            newDate = newDate.AddMonths(1);
        newDate = CalculateMonthFirstDate(newDate);
        return newDate;
    }
    public static DateTime CalculatePrevMonthStartDate(DateTime startDate) {
        DateTime newDate = startDate.AddMonths(-1);
        if (newDate.Day > 22)
            newDate = newDate.AddMonths(1);
        newDate = CalculateMonthFirstDate(newDate);
        return newDate;
    }
    public static DateTime CalculateMonthFirstDate(DateTime newDate) {
        DateTime result;
        result = newDate.Date.AddDays(1 - newDate.Day);
        return result;
    }
}