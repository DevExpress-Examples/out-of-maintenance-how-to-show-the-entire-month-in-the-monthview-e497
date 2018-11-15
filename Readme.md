<!-- default file list -->
*Files to look at*:

* [MyCallbackCommands.cs](./CS/WebSite/App_Code/MyCallbackCommands.cs) (VB: [MyCallbackCommands.vb](./VB/WebSite/App_Code/MyCallbackCommands.vb))
* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
<!-- default file list end -->
# How to show the entire month in the MonthView


<p>Problem:</p><p>Is there a way to always show the full month in month view? I would like to see May 1st through May 31st, not the week starting off in the week that we are currently in the month. Like in MS Outlook, when I click the next month I see the entire month.</p><p>Solution:</p><p>Use custom NavigateForwardCallbackCommand and NavigateBackwardCallbackCommand commands, and then adjust the ASPxScheduler.Start property value according to your scenario.</p>

<br/>


