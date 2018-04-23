using System;
using System.Data;
using System.Configuration;
using System.Web;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;


public class MyNavigateForwardCallbackCommand : NavigateForwardCallbackCommand
{
    public MyNavigateForwardCallbackCommand(ASPxScheduler control)
        : base(control)
    {
    }

    protected override void ExecuteCore()
    {
        DateTime plusMonthDate = Control.Start.Date.AddMonths(1);
        if (plusMonthDate.Day > 22)
            plusMonthDate = plusMonthDate.Date.AddMonths(1).AddDays(1 - plusMonthDate.Day);
        else
            plusMonthDate = plusMonthDate.Date.AddDays(1 - plusMonthDate.Day);
        base.ExecuteCore();

        if (Control.ActiveViewType == SchedulerViewType.Month)
            Control.Start = plusMonthDate;

    }
}