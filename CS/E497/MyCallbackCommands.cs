
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


public class MySwitchViewCallbackCommand : SwitchViewCallbackCommand {

    public MySwitchViewCallbackCommand(ASPxScheduler control)
        : base(control) {
    }
    protected override void ExecuteCore() {
        base.ExecuteCore();
        if(Control.ActiveViewType == SchedulerViewType.Month) {
            Control.Start = DateTimeNavigationHelper.CalculateMonthFirstDate(Control.Start);
        }
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
public sealed class DateTimeNavigationHelper {

    private DateTimeNavigationHelper() {
    }

    public static System.DateTime CalculateNextMonthStartDate(System.DateTime startDate) {
        System.DateTime newDate = startDate.AddMonths(1);
        if(newDate.Day > 22) {
            newDate = newDate.AddMonths(1);
        }
        newDate = CalculateMonthFirstDate(newDate);
        return newDate;
    }
    public static System.DateTime CalculatePrevMonthStartDate(System.DateTime startDate) {
        System.DateTime newDate = startDate.AddMonths(-1);
        if(newDate.Day > 22) {
            newDate = newDate.AddMonths(1);
        }
        newDate = CalculateMonthFirstDate(newDate);
        return newDate;
    }
    public static System.DateTime CalculateMonthFirstDate(System.DateTime newDate) {
        System.DateTime result = default(System.DateTime);
        result = newDate.Date.AddDays(1 - newDate.Day);
        return result;
    }
}