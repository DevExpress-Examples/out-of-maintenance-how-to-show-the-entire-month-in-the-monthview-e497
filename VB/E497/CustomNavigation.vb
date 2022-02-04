Imports Microsoft.VisualBasic
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web.ASPxScheduler.Internal
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Commands
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Diagnostics
Imports System.Threading

Public Class MonthViewHelper
	Public Shared Function CalculateWeekCount(ByVal scheduler As ASPxScheduler, ByVal monthStart As DateTime) As Integer
		Dim localDoW As DayOfWeek = If(scheduler.OptionsView.FirstDayOfWeek = DevExpress.XtraScheduler.FirstDayOfWeek.System, Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek, CType(scheduler.OptionsView.FirstDayOfWeek, DayOfWeek))

		Dim weekStart As DateTime = FirstDayOfWeek(monthStart, localDoW)
		Dim monthEnd As DateTime = monthStart.AddMonths(1)
		Dim weekCount As Integer = 0
		Do While weekStart < monthEnd
			weekCount += 1
			weekStart = weekStart.AddDays(7)
		Loop
		Return weekCount
	End Function

	Public Shared Function FirstDayOfWeek(ByVal [date] As DateTime, ByVal startOfWeek As DayOfWeek) As DateTime
		Dim diff As Integer = (7 + ([date].DayOfWeek - startOfWeek)) Mod 7
		Return [date].AddDays(-1 * diff).Date
	End Function
End Class

Public Class CustomOffsetVisibleIntervalsCallbackCommand
	Inherits OffsetVisibleIntervalsCallbackCommand
	Public Sub New(ByVal scheduler As ASPxScheduler)
		MyBase.New(scheduler)
	End Sub

	Protected Overrides Sub ExecuteCore()
		'base.ExecuteCore();
		Dim currentMonth As DateTime = FirstDate.AddDays(15)
		currentMonth = New DateTime(currentMonth.Year, currentMonth.Month, 1)
		Control.Start = currentMonth
		Control.MonthView.WeekCount = MonthViewHelper.CalculateWeekCount(Control, currentMonth)
	End Sub
End Class
Public Class CustomNavigateForwardCallbackCommand
	Inherits NavigateForwardCallbackCommand
	Public Sub New(ByVal scheduler As ASPxScheduler)
		MyBase.New(scheduler)
	End Sub

	Protected Overrides Sub ExecuteCore()
		'base.ExecuteCore();
		Dim currentMonth As DateTime = Control.Start.AddDays(15)
		currentMonth = New DateTime(currentMonth.Year, currentMonth.Month, 1)
		Dim newStartDate As DateTime = currentMonth.AddMonths(1)
		Control.Start = newStartDate
		Control.MonthView.WeekCount = MonthViewHelper.CalculateWeekCount(Control, newStartDate)
	End Sub
End Class

Public Class CustomNavigateBackwardCallbackCommand
	Inherits NavigateBackwardCallbackCommand
	Public Sub New(ByVal scheduler As ASPxScheduler)
		MyBase.New(scheduler)
	End Sub

	Protected Overrides Sub ExecuteCore()
		'base.ExecuteCore();
		Dim currentMonth As DateTime = Control.Start.AddDays(15)
		currentMonth = New DateTime(currentMonth.Year, currentMonth.Month, 1)
		Dim newStartDate As DateTime = currentMonth.AddMonths(-1)
		Control.Start = newStartDate
		Control.MonthView.WeekCount = MonthViewHelper.CalculateWeekCount(Control, newStartDate)
	End Sub
End Class

Public Class CustomGotoTodayCallbackCommand
	Inherits GotoTodayCallbackCommand
	Public Sub New(ByVal scheduler As ASPxScheduler)
		MyBase.New(scheduler)
	End Sub

	Protected Overrides Sub ExecuteCore()
		'base.ExecuteCore();
		Dim newStartDate As New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
		Control.Start = newStartDate
		Control.MonthView.WeekCount = MonthViewHelper.CalculateWeekCount(Control, newStartDate)
	End Sub
End Class

Public Class CustomGotoDateDialogCommand
	Inherits DevExpress.Web.ASPxScheduler.Internal.GotoDateFormCallbackCommand
	Public Sub New(ByVal scheduler As ASPxScheduler)
		MyBase.New(scheduler)
	End Sub

	Protected Overrides Sub ExecuteCore()
		Dim newStartDate As New DateTime(NewDate.Date.Year, NewDate.Date.Month, 1)
		Control.Start = newStartDate
		Control.MonthView.WeekCount = MonthViewHelper.CalculateWeekCount(Control, newStartDate)
	End Sub
End Class

Public Class CustomGotoDateCallbackCommand
	Inherits GotoDateCallbackCommand
	Public Sub New(ByVal scheduler As ASPxScheduler)
		MyBase.New(scheduler)
	End Sub

	Protected Overrides Sub ExecuteCore()
		'base.ExecuteCore();
		Dim newStartDate As New DateTime(NewDate.Year, NewDate.Month, 1)
		Control.Start = newStartDate
		Control.MonthView.WeekCount = MonthViewHelper.CalculateWeekCount(Control, newStartDate)
	End Sub
End Class

Public Class CustomSwitchViewCallbackCommand
	Inherits SwitchViewCallbackCommand
	Public Sub New(ByVal scheduler As ASPxScheduler)
		MyBase.New(scheduler)
	End Sub

	Protected Overrides Sub ExecuteCore()
		MyBase.ExecuteCore()
		If Control.ActiveViewType = SchedulerViewType.Month Then
			Dim newStartDate As New DateTime(Control.Start.Year, Control.Start.Month, 1)
			Control.Start = newStartDate
			Control.MonthView.WeekCount = MonthViewHelper.CalculateWeekCount(Control, newStartDate)
		End If
	End Sub
End Class