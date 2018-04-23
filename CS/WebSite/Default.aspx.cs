using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler.Services;
using DevExpress.XtraScheduler.Commands;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Native;
using DevExpress.Web.ASPxScheduler.Internal;

public partial class _Default : System.Web.UI.Page {

    protected void Page_Init(object sender, EventArgs e) {
        IDateTimeNavigationService service = ASPxScheduler1.GetService(typeof(IDateTimeNavigationService)) as IDateTimeNavigationService;
        ASPxScheduler1.RemoveService(typeof(IDateTimeNavigationService));
        ASPxScheduler1.AddService(typeof(IDateTimeNavigationService), new MyDateTimeNavigationService(ASPxScheduler1, service));
    }
    protected void Page_Load(object sender, EventArgs e) {
        if (ASPxScheduler1.ActiveViewType == SchedulerViewType.Month) {
             if (NeedDateCorrection(ASPxScheduler1.MonthView.GetVisibleIntervals()))
                 ASPxScheduler1.Start = DateTimeNavigationHelper.CalculateMonthFirstDate(ASPxScheduler1.Start);
        }
    }
    bool NeedDateCorrection(TimeIntervalCollection timeIntervalCollection) {
       TimeInterval firstWeekInterval = timeIntervalCollection[0];
       return firstWeekInterval.Start.Day != 1 && firstWeekInterval.Start.Month == firstWeekInterval.End.Month;
    }
    protected void ASPxScheduler1_BeforeExecuteCallbackCommand(object sender, DevExpress.Web.ASPxScheduler.SchedulerCallbackCommandEventArgs e) {
        ASPxScheduler scheduler = (ASPxScheduler)sender;
        if (scheduler.ActiveViewType == SchedulerViewType.Month) {
            if (e.CommandId == SchedulerCallbackCommandId.NavigateForward)
                e.Command = new MyNavigateForwardCallbackCommand((ASPxScheduler)sender);
            else if (e.CommandId == SchedulerCallbackCommandId.NavigateBackward)
                e.Command = new MyNavigateBackwardCallbackCommand((ASPxScheduler)sender);
        }
        if (e.CommandId == SchedulerCallbackCommandId.SwitchView)
            e.Command = new MySwitchViewCallbackCommand((ASPxScheduler)sender);
    }
}
public class MyDateTimeNavigationService : IDateTimeNavigationService {
    ASPxScheduler control;
    IDateTimeNavigationService baseService;

    public MyDateTimeNavigationService(ASPxScheduler control, IDateTimeNavigationService baseService) {
        if (control == null)
            Exceptions.ThrowArgumentNullException("control");
        this.control = control;
        this.baseService = baseService;
    }

    public ASPxScheduler Control { get { return control; } }
    public IDateTimeNavigationService BaseService { get { return baseService; } }

    #region IDateTimeNavigationService Members
    public void GoToToday() {
        if (UseBaseService()) {
            BaseService.GoToToday();
            return;
        }
        SchedulerTimeZone serverTimeZone = SchedulerTimeZoneHelper.Instance.CurrentTimeZone;
        SchedulerTimeZone clientTimeZone = SchedulerTimeZoneHelper.Instance.FindTimeZoneById(Control.OptionsBehavior.ClientTimeZoneId);
        DateTime clientToday = TimeZoneHelper.ConvertDate(DateTime.Now, serverTimeZone, clientTimeZone);
        GoToDate(clientToday.Date);
    }
    public void GoToDate(DateTime date) {
        GoToDate(date, control.ActiveViewType);
    }
    public void GoToDate(DateTime date, SchedulerViewType viewType) {
        if (UseBaseService()) {
            BaseService.GoToDate(date, viewType);
            return;
        }
        BaseService.GoToDate(date, viewType);
        Control.Start = DateTimeNavigationHelper.CalculateMonthFirstDate(date);
    }
    public void NavigateForward() {
        if (UseBaseService()) {
            BaseService.NavigateForward();
            return;
        }
        MyNavigateViewForwardCommand cmd = new MyNavigateViewForwardCommand(control);
        cmd.Execute();
    }
    public void NavigateBackward() {
        if (UseBaseService()) {
            BaseService.NavigateBackward();
            return;
        }
        MyNavigateViewBackwardCommand cmd = new MyNavigateViewBackwardCommand(control);
        cmd.Execute();
    }
    bool UseBaseService() {
        return Control.ActiveViewType != SchedulerViewType.Month;
    }
    #endregion
}
