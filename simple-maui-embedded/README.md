## Simple MAUI Embedded

The simple-maui-embedded directory contains the solution for running native .NET Android or iOS with the embedded MAUI use case within the single project structure. Even though we don't use the single project structure, one Android issue we've run into is unique in that it works OK under a single project structure but not when the native Android code is in its own project.

Running this will allow one to test the different issues we've encountered on Android or iOS. In some cases, only Android has the bug. In other cases both Android and iOS experience the same bugs.

In general, one can use this to try and repro:

* Illustrating that the Appearing\Disappearing events do not fire when using MAUI embedded nor do the display alerts and action sheets work when using MAUI embedded for both Android and iOS.
* Illustrate trying to display an image using a glyph from a font icon file does not work in Android if trying to use the MAUI Context instance that was created when the application first loads if the initial Activity has been Finished (such as when going from a Login activity to a secured content activity).
* Illustrate a grouped list runs OK in Android if Android is within the single project structure.
* Illustrate the font icon and grouped list are not issues in iOS and run OK.
