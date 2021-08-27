<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128547717/14.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E497)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Default.aspx](./CS/E497/Default.aspx)
* [Default.aspx.cs](./CS/E497/Default.aspx.cs)
* [Global.asax](./CS/E497/Global.asax)
* [Global.asax.cs](./CS/E497/Global.asax.cs)
* [MyCallbackCommands.cs](./CS/E497/MyCallbackCommands.cs)
<!-- default file list end -->
# How to show the entire month in the MonthView
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/e497/)**
<!-- run online end -->


<p>Problem:</p><p>Is there a way to always show the full month in month view? I would like to see May 1st through May 31st, not the week starting off in the week that we are currently in the month. Like in MS Outlook, when I click the next month I see the entire month.</p><p>Solution:</p><p>Use custom NavigateForwardCallbackCommand and NavigateBackwardCallbackCommand commands, and then adjust the ASPxScheduler.Start property value according to your scenario.</p>

<br/>


