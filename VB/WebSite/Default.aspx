<%@ Page Language="vb" AutoEventWireup="true"  CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v7.3, Version=7.3.6.0, Culture=neutral, PublicKeyToken=9B171C9FD64DA1D1"
	Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v7.3.Core, Version=7.3.6.0, Culture=neutral, PublicKeyToken=9B171C9FD64DA1D1"
	Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>Untitled Page</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" ActiveViewType="Month"
			OnBeforeExecuteCallbackCommand="ASPxScheduler1_BeforeExecuteCallbackCommand">
			<Views>
				<DayView>
					<TimeRulers>
						<cc1:TimeRuler>
						</cc1:TimeRuler>
					</TimeRulers>
				</DayView>
				<WorkWeekView>
					<TimeRulers>
						<cc1:TimeRuler>
						</cc1:TimeRuler>
					</TimeRulers>
				</WorkWeekView>
			</Views>
		</dxwschs:ASPxScheduler>

	</div>
	</form>
</body>
</html>
