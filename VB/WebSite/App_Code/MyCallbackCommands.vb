Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports DevExpress.Web.ASPxScheduler.Internal
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.XtraScheduler


Public Class MyNavigateForwardCallbackCommand
	Inherits NavigateForwardCallbackCommand
	Public Sub New(ByVal control As ASPxScheduler)
		MyBase.New(control)
	End Sub

	Protected Overrides Sub ExecuteCore()
		Dim plusMonthDate As DateTime = Control.Start.Date.AddMonths(1)
		If plusMonthDate.Day > 22 Then
			plusMonthDate = plusMonthDate.Date.AddMonths(1).AddDays(1 - plusMonthDate.Day)
		Else
			plusMonthDate = plusMonthDate.Date.AddDays(1 - plusMonthDate.Day)
		End If
		MyBase.ExecuteCore()

		If Control.ActiveViewType = SchedulerViewType.Month Then
			Control.Start = plusMonthDate
		End If

	End Sub
End Class