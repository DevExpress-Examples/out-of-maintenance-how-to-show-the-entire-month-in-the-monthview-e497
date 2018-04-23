# How to show the entire month in the MonthView


<p>Problem:</p><p>Is there a way to always show the full month in month view? I would like to see May 1st through May 31st, not the week starting off in the week that we are currently in the month. Like in MS Outlook, when I click the next month I see the entire month.</p><p>Solution:</p><p>Use custom NavigateForwardCallbackCommand and NavigateBackwardCallbackCommand commands, and then adjust the ASPxScheduler.Start property value according to your scenario.</p>

<br/>


