Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports DevExpress.Web.ASPxScheduler.Internal
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Commands

Public Class MySwitchViewCallbackCommand
    Inherits SwitchViewCallbackCommand

    Public Sub New(ByVal control As ASPxScheduler)
        MyBase.New(control)
    End Sub
    Protected Overrides Sub ExecuteCore()
        MyBase.ExecuteCore()
        If Control.ActiveViewType = SchedulerViewType.Month Then
            Control.Start = DateTimeNavigationHelper.CalculateMonthFirstDate(Control.Start)
        End If
    End Sub
End Class
Public Class MyNavigateForwardCallbackCommand
    Inherits NavigateForwardCallbackCommand

    Public Sub New(ByVal control As ASPxScheduler)
        MyBase.New(control)
    End Sub
    Protected Overrides Function CreateCommand() As SchedulerCommand
        Return New MyNavigateViewForwardCommand(Control)
    End Function
End Class
Public Class MyNavigateBackwardCallbackCommand
    Inherits NavigateBackwardCallbackCommand

    Public Sub New(ByVal control As ASPxScheduler)
        MyBase.New(control)
    End Sub
    Protected Overrides Function CreateCommand() As SchedulerCommand
        Return New MyNavigateViewBackwardCommand(Control)
    End Function
End Class
Public Class MyNavigateViewForwardCommand
    Inherits NavigateViewForwardCommand

    Public Sub New(ByVal target As ISchedulerCommandTarget)
        MyBase.New(target)
    End Sub
    Protected Overrides Sub ExecuteCore()
        InnerControl.Start = DateTimeNavigationHelper.CalculateNextMonthStartDate(InnerControl.Start)
    End Sub
End Class
Public Class MyNavigateViewBackwardCommand
    Inherits NavigateViewBackwardCommand

    Public Sub New(ByVal target As ISchedulerCommandTarget)
        MyBase.New(target)
    End Sub
    Protected Overrides Sub ExecuteCore()
        InnerControl.Start = DateTimeNavigationHelper.CalculatePrevMonthStartDate(InnerControl.Start)
    End Sub
End Class
Public NotInheritable Class DateTimeNavigationHelper

    Private Sub New()
    End Sub

    Public Shared Function CalculateNextMonthStartDate(ByVal startDate As Date) As Date
        Dim newDate As Date = startDate.AddMonths(1)
        If newDate.Day > 22 Then
            newDate = newDate.AddMonths(1)
        End If
        newDate = CalculateMonthFirstDate(newDate)
        Return newDate
    End Function
    Public Shared Function CalculatePrevMonthStartDate(ByVal startDate As Date) As Date
        Dim newDate As Date = startDate.AddMonths(-1)
        If newDate.Day > 22 Then
            newDate = newDate.AddMonths(1)
        End If
        newDate = CalculateMonthFirstDate(newDate)
        Return newDate
    End Function
    Public Shared Function CalculateMonthFirstDate(ByVal newDate As Date) As Date
        Dim result As Date
        result = newDate.Date.AddDays(1 - newDate.Day)
        Return result
    End Function
End Class