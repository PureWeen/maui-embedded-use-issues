The `simple-maui-staticresource-fail` directory contains the solution for running a native .NET Android or .NET iOS application with embedded MAUI to illustrate some of the odd and sporadic issues that crash the app when
trying to access a StaticResource in a global merged resource dictionary.

This app is the single project structure for simplicity, but our actual production application is comprised of individual .NET Android and .NET iOS projects refernencing a shared MAUI library and we see the same issues there. 

The issues only appear to impact iOS. Android is included simply to illustrate it runs with no issues.

**IMPORTANT:** When running the iOS application in debugging, you will need to stop and restart the application when trying the different options as the crashing issues ONLY occur if that page is the first MAUI page loaded. If a different MAUI page has loaded first, then the issues do not seem to occur, for the most part.

There are two issues we've encountered that this repros:
1. In iOS, if a page contains a list with a data template, and the template contains an element (such as a label) that references a static resource (such as for a style), then the page will crash saying it cannot find the static resource if it's the first page loaded. If the page however is navigated to from a different MAUI page that loaded OK, then we do not see the crash. Also, if one uses DynamicResource instead of StaticResource it seems to load fine as the first page and does not crash.
2. In iOS, sometimes (it can be sporadic but almost always seems to happen when going from the first loaded page to the second) the resource dictionaries that were added as merged resources at the application level seem to be lost and we get an error when trying to access a keyed resource.
