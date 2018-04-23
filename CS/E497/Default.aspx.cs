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
            IDateTimeNavigationService service = ASPxScheduler1.GetService(typeof(IDateTimeNavigationService)) as IDateTimeNavigationService;
            ASPxScheduler1.RemoveService(typeof(IDateTimeNavigationService));
            ASPxScheduler1.AddService(typeof(IDateTimeNavigationService), new MyDateTimeNavigationService(ASPxScheduler1, service));
        }
        
        protected void Page_Load(object sender, EventArgs e) {
            if(ASPxScheduler1.ActiveViewType == SchedulerViewType.Month) {
                if(NeedDateCorrection(ASPxScheduler1.MonthView.GetVisibleIntervals())) {
                    ASPxScheduler1.Start = DateTimeNavigationHelper.CalculateMonthFirstDate(ASPxScheduler1.Start);
                }
            }
        }

        private bool NeedDateCorrection(TimeIntervalCollection timeIntervalCollection) {
            TimeInterval firstWeekInterval = timeIntervalCollection[0];
            return firstWeekInterval.Start.Day != 1 && firstWeekInterval.Start.Month == firstWeekInterval.End.Month;
        }


        protected void ASPxScheduler1_BeforeExecuteCallbackCommand(object sender, DevExpress.Web.ASPxScheduler.SchedulerCallbackCommandEventArgs e) {
            ASPxScheduler scheduler = (ASPxScheduler)sender;
            if(scheduler.ActiveViewType == SchedulerViewType.Month) {
                if(e.CommandId == SchedulerCallbackCommandId.NavigateForward) {
                    e.Command = new MyNavigateForwardCallbackCommand((ASPxScheduler)sender);
                } else if(e.CommandId == SchedulerCallbackCommandId.NavigateBackward) {
                    e.Command = new MyNavigateBackwardCallbackCommand((ASPxScheduler)sender);
                }
            }
            if(e.CommandId == SchedulerCallbackCommandId.SwitchView) {
                e.Command = new MySwitchViewCallbackCommand((ASPxScheduler)sender);
            }
        }
    }



public class MyDateTimeNavigationService : IDateTimeNavigationService
{
	private ASPxScheduler control_Renamed;

	private IDateTimeNavigationService baseService_Renamed;
	public MyDateTimeNavigationService(ASPxScheduler control, IDateTimeNavigationService baseService)
	{
		if (control == null) {
			Exceptions.ThrowArgumentNullException("control");
		}
		this.control_Renamed = control;
		this.baseService_Renamed = baseService;
	}

	public ASPxScheduler Control {
		get { return control_Renamed; }
	}
	public IDateTimeNavigationService BaseService {
		get { return baseService_Renamed; }
	}

	#region "IDateTimeNavigationService Members"
	public void GoToToday()
	{
		if (UseBaseService()) {
			BaseService.GoToToday();
			return;
		}

		TimeZoneInfo serverTimeZone = TimeZoneInfo.Local;
		TimeZoneInfo clientTimeZone = TimeZoneInfo.FindSystemTimeZoneById(Control.OptionsBehavior.ClientTimeZoneId);
		System.DateTime clientToday = TimeZoneInfo.ConvertTime(System.DateTime.Now, serverTimeZone, clientTimeZone);
		GoToDate(clientToday.Date);
	}
	public void GoToDate(System.DateTime date)
	{
		GoToDate(date, control_Renamed.ActiveViewType);
	}
	public void GoToDate(System.DateTime date, SchedulerViewType viewType)
	{
		if (UseBaseService()) {
			BaseService.GoToDate(date, viewType);
			return;
		}
		BaseService.GoToDate(date, viewType);
		Control.Start = DateTimeNavigationHelper.CalculateMonthFirstDate(date);
	}
	public void NavigateForward()
	{
		if (UseBaseService()) {
			BaseService.NavigateForward();
			return;
		}
		MyNavigateViewForwardCommand cmd = new MyNavigateViewForwardCommand(control_Renamed);
		cmd.Execute();
	}
	public void NavigateBackward()
	{
		if (UseBaseService()) {
			BaseService.NavigateBackward();
			return;
		}
		MyNavigateViewBackwardCommand cmd = new MyNavigateViewBackwardCommand(control_Renamed);
		cmd.Execute();
	}
	private bool UseBaseService()
	{
		return Control.ActiveViewType != SchedulerViewType.Month;
	}
	#endregion
}
}