Imports Microsoft.VisualBasic
Imports DevExpress.Office.Utils
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Services
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace T195089
	Partial Public Class [Default]
		Inherits System.Web.UI.Page
		Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
			Dim currentMonth As New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
			ASPxScheduler1.Start = currentMonth
			ASPxScheduler1.MonthView.WeekCount = MonthViewHelper.CalculateWeekCount(ASPxScheduler1, currentMonth)
		End Sub

		Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

		End Sub

		Protected Sub ASPxScheduler1_BeforeExecuteCallbackCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxScheduler.SchedulerCallbackCommandEventArgs)
			Dim scheduler = TryCast(sender, ASPxScheduler)
			If scheduler.ActiveViewType = SchedulerViewType.Month Then
				If e.CommandId = SchedulerCallbackCommandId.NavigateForward Then
					e.Command = New CustomNavigateForwardCallbackCommand(TryCast(sender, ASPxScheduler))
				End If
				If e.CommandId = SchedulerCallbackCommandId.NavigateBackward Then
					e.Command = New CustomNavigateBackwardCallbackCommand(TryCast(sender, ASPxScheduler))
				End If
				If e.CommandId = SchedulerCallbackCommandId.GotoToday Then
					e.Command = New CustomGotoTodayCallbackCommand(TryCast(sender, ASPxScheduler))
				End If
				If e.CommandId = SchedulerCallbackCommandId.GotoDateForm Then
					e.Command = New CustomGotoDateDialogCommand(TryCast(sender, ASPxScheduler))
				End If
				If e.CommandId = SchedulerCallbackCommandId.GotoDate Then
					e.Command = New CustomGotoDateCallbackCommand(TryCast(sender, ASPxScheduler))
				End If
				If e.CommandId = SchedulerCallbackCommandId.OffsetVisibleIntervals Then
					e.Command = New CustomOffsetVisibleIntervalsCallbackCommand(TryCast(sender, ASPxScheduler))
				End If
			End If
			If e.CommandId = SchedulerCallbackCommandId.SwitchView Then
				e.Command = New CustomSwitchViewCallbackCommand(TryCast(sender, ASPxScheduler))
			End If
		End Sub
	End Class
End Namespace