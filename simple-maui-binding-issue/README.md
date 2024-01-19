The `simple-maui-binding-issue` directory contains the solution illustrating a repro of a truly odd bug impacting embedded MAUI in iOS when using a ControlTemplate applied to all the MAUI pages, such as for ensuring every page displays a custom navigation bar.

The bug is none of the bindings on the content page loaded in the content presenter of the template work the first time we navigate from one MAUI page to the next if any kind of modal controller was displayed in iOS first (alert, action sheet, etc.).

So take the navigation pattern of Native iOS Controller -> Maui Page One -> Maui Page Two. Now, presume that before navigating to Maui Page One an alert was displayed communicating we were loading some data before navigating.
In this scenario, the bindings on MAUI Page One work OK. When then navigating to MAUI Page Two, none of the bindings will work on MAUI Page Two - no data is displayed, no commands work on tap, etc.
If we navigate back to Maui Page One and then to Maui Page Two, then all the bindings work fine and we see the data on the page we would expect. This ONLY happens on that first navigation to MAUI Page Two and ONLY if some form of 
Modal Dialog controller was displayed first on the Native iOS controller.

One can experiment with these different scenarios by assigning the desired constant to `demoPathToRun` in `ButtonEventDemoViewController` and then debugging the app on an iOS simulator again. Remember, this bug only occurs 
the first time we navigate from MAUI Page One to MAUI Page Two.

```
      
    const int DemoFailWithLoadingDialogPreNavigation = 1;
    
    const int DemoFailWithPresentingActionSheet = 2;
    
    const int DemoSucessWithSimplePushViewController = 3;
    
    const int DemoFailWithPresentingViewControllerModal = 4;
    
    const int DemoSuccessWithPresentingViewControllerFullScreen = 5;
    
    // Change the option here and run again to explore the different scenarios.
    int demoPathToRun = DemoFailWithLoadingDialogPreNavigation;
    
    switch (demoPathToRun)
```
