Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.XtraScheduler.Services
Imports DevExpress.XtraScheduler.Commands
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Native
Imports DevExpress.Web.ASPxScheduler.Internal

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim service As IDateTimeNavigationService = TryCast(ASPxScheduler1.GetService(GetType(IDateTimeNavigationService)), IDateTimeNavigationService)
        ASPxScheduler1.RemoveService(GetType(IDateTimeNavigationService))
        ASPxScheduler1.AddService(GetType(IDateTimeNavigationService), New MyDateTimeNavigationService(ASPxScheduler1, service))
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If ASPxScheduler1.ActiveViewType = SchedulerViewType.Month Then
             If NeedDateCorrection(ASPxScheduler1.MonthView.GetVisibleIntervals()) Then
                 ASPxScheduler1.Start = DateTimeNavigationHelper.CalculateMonthFirstDate(ASPxScheduler1.Start)
             End If
        End If
    End Sub
    Private Function NeedDateCorrection(ByVal timeIntervalCollection As TimeIntervalCollection) As Boolean
       Dim firstWeekInterval As TimeInterval = timeIntervalCollection(0)
       Return firstWeekInterval.Start.Day <> 1 AndAlso firstWeekInterval.Start.Month = firstWeekInterval.End.Month
    End Function
    Protected Sub ASPxScheduler1_BeforeExecuteCallbackCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxScheduler.SchedulerCallbackCommandEventArgs)
        Dim scheduler As ASPxScheduler = DirectCast(sender, ASPxScheduler)
        If scheduler.ActiveViewType = SchedulerViewType.Month Then
            If e.CommandId = SchedulerCallbackCommandId.NavigateForward Then
                e.Command = New MyNavigateForwardCallbackCommand(DirectCast(sender, ASPxScheduler))
            ElseIf e.CommandId = SchedulerCallbackCommandId.NavigateBackward Then
                e.Command = New MyNavigateBackwardCallbackCommand(DirectCast(sender, ASPxScheduler))
            End If
        End If
        If e.CommandId = SchedulerCallbackCommandId.SwitchView Then
            e.Command = New MySwitchViewCallbackCommand(DirectCast(sender, ASPxScheduler))
        End If
    End Sub
End Class
Public Class MyDateTimeNavigationService
    Implements IDateTimeNavigationService


    Private control_Renamed As ASPxScheduler

    Private baseService_Renamed As IDateTimeNavigationService

    Public Sub New(ByVal control As ASPxScheduler, ByVal baseService As IDateTimeNavigationService)
        If control Is Nothing Then
            Exceptions.ThrowArgumentNullException("control")
        End If
        Me.control_Renamed = control
        Me.baseService_Renamed = baseService
    End Sub

    Public ReadOnly Property Control() As ASPxScheduler
        Get
            Return control_Renamed
        End Get
    End Property
    Public ReadOnly Property BaseService() As IDateTimeNavigationService
        Get
            Return baseService_Renamed
        End Get
    End Property

    #Region "IDateTimeNavigationService Members"
    Public Sub GoToToday() Implements IDateTimeNavigationService.GoToToday
        If UseBaseService() Then
            BaseService.GoToToday()
            Return
        End If

        Dim serverTimeZone As TimeZoneInfo = TimeZoneInfo.Local
        Dim clientTimeZone As TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(Control.OptionsBehavior.ClientTimeZoneId)
        Dim clientToday As Date = TimeZoneInfo.ConvertTime(Date.Now, serverTimeZone, clientTimeZone)
        GoToDate(clientToday.Date)
    End Sub
    Public Sub GoToDate(ByVal [date] As Date) Implements IDateTimeNavigationService.GoToDate
        GoToDate([date], control_Renamed.ActiveViewType)
    End Sub
    Public Sub GoToDate(ByVal [date] As Date, ByVal viewType As SchedulerViewType) Implements IDateTimeNavigationService.GoToDate
        If UseBaseService() Then
            BaseService.GoToDate([date], viewType)
            Return
        End If
        BaseService.GoToDate([date], viewType)
        Control.Start = DateTimeNavigationHelper.CalculateMonthFirstDate([date])
    End Sub
    Public Sub NavigateForward() Implements IDateTimeNavigationService.NavigateForward
        If UseBaseService() Then
            BaseService.NavigateForward()
            Return
        End If
        Dim cmd As New MyNavigateViewForwardCommand(control_Renamed)
        cmd.Execute()
    End Sub
    Public Sub NavigateBackward() Implements IDateTimeNavigationService.NavigateBackward
        If UseBaseService() Then
            BaseService.NavigateBackward()
            Return
        End If
        Dim cmd As New MyNavigateViewBackwardCommand(control_Renamed)
        cmd.Execute()
    End Sub
    Private Function UseBaseService() As Boolean
        Return Control.ActiveViewType <> SchedulerViewType.Month
    End Function
    #End Region
End Class
