## Table of Contents

* [Overview](#overview)
* [Disclaimer](#disclaimer)
* [Simple MAUI First App](#simple-maui-first-app)
* [Simple MAUI Embedded](#simple-maui-embedded)
* [Simple Android Embedded](#simple-android-embedded)
* [Simple MAUI StaticResource Fail](#simple-maui-staticresource-fail)
* [Simple MAUI Binding Issue](#simple-maui-binding-issue)
* [Simple MAUI Core](#simple-maui-core)

## Overview

This repo is intended to house simplified examples of using MAUI under the use case of interacting with MAUI pages embedded in native .NET Android and .NET iOS applications for the purposes of reproducing the various bugs we have encountered when trying to 
migrate from Xamarin Forms embedded in native Xamarin Android and Xamain iOS.

Our structure when using Xamarin was:
* Our Xamarin Forms code (pages, view models, etc.) resided in a .NET Standard project.
* We had a separate project for our Xamarin Android application that had a dependency on the shared Xamarin Forms project.
* We had a separate project for our Xamarin iOS application that had a dependency on the shared Xamarin Forms project.

While attempting to migrate to MAUI and .NET 7, our structure remains the same:
* Our existing shared project for Xamrin Forms code has been updated to MAUI targeting .net-android and .net-ios.
* Our separate project for Android has been updated to .NET7 Android and has a dependency on the shared MAUI project.
* Our separate project for iOS has been updated to .NET7 iOS and has a dependency on the shared MAUI project.

We do not and cannot at this time use the single project approach where the Android and iOS specific code all resides in one project under Platforms.

## Disclaimer ##

All of the code in this repo is VERY much quick trash to simply provide quick code examples that reproduce the various MAUI bugs we're encountering so we can file bugs.
Some of the examples use view models along with the pages, others simply bind to the page code behind. In some cases we've copied over relevant portions of our production code in whole or in part
to set up any framework level stuff (navigation, initializations, etc.) but it is no means complete or necessarily clean. I have not bothered with XML Doc either.

None of this is meant to reflect actual production ready code nor illustrate proper use\patterns\etc. As such, the contents of this repo should not be considered a reflection of how I actually write code! I'd never in a million years put this junk into production!

With that out of the way, carry on...

## Simple MAUI First App

The `simple-maui-first-app` directory contains the solution for running a pure MAUI application to illustrate some of the issues we're seeing with embedded MAUI work correctly when it's pure MAUI.
Specifically, this will illustrate the Appearing\Disappearing events fire as expected on the Page and the MAUI methods for displaying alerts or action sheets work as expected.

## Simple MAUI Embedded

The `simple-maui-embedded` directory contains the solution for running native .NET Android or iOS with the embedded MAUI use case within the single project structure. 
Even though we don't use the single project structure, one Android issue we've run into is unique in that it works OK under a single project structure but not when the native Android code is in its own project.

Running this will allow one to test the different issues we've encountered on Android or iOS. In some cases, only Android has the bug. In other cases both Android and iOS experience the same bugs.

In general, one can use this to try and repro:
* Illustrating that the Appearing\Disappearing events do not fire when using MAUI embedded nor do the display alerts and action sheets work when using MAUI embedded for both Android and iOS.
* Illustrate trying to display an image using a glyph from a font icon file does not work in Android if trying to use the MAUI Context instance that was created when the application first loads if the initial Activity has been Finished (such as when going from a Login activity to a secured content activity).
* Illustrate a grouped list runs OK in Android if Android is within the single project structure.
* Illustrate the font icon and grouped list are not issues in iOS and run OK.

## Simple Android Embedded

The `simple-android-embedded` directory contains the solution for running a native .NET Android application with embedded MAUI outside of the single project structure (better representing how we actually use it).

This supports the same actions as 'simple-maui-embedded' but will show that trying to use a grouped list throws an exception in Android outside of the signle project structure.

## Simple MAUI StaticResource Fail

The `simple-maui-staticresource-fail` directory contains the solution for running a native .NET Android or .NET iOS application with embedded MAUI to illustrate some of the odd and sporadic issues that crash the app when
trying to access a StaticResource in a global merged resource dictionary.

This app is the single project structure for simplicity, but our actual production application is comprised of individual .NET Android and .NET iOS projects referencing a shared MAUI library and we see the same issues there. 

The issues only appear to impact iOS. Android is included simply to illustrate it runs with no issues.

**IMPORTANT:** When running the iOS application in debugging, you will need to stop and restart the application when trying the different options as the crashing issues ONLY occur if that page is the first MAUI page loaded. If a different MAUI page has loaded first, then the issues do not seem to occur, for the most part.

There are two issues we've encountered that this repros:
1. In iOS, if a page contains a list with a data template, and the template contains an element (such as a label) that references a static resource (such as for a style), then the page will crash saying it cannot find the static resource if it's the first page loaded. If the page however is navigated to from a different MAUI page that loaded OK, then we do not see the crash. Also, if one uses DynamicResource instead of StaticResource it seems to load fine as the first page and does not crash.
2. In iOS, sometimes (it can be sporadic but almost always seems to happen when going from the first loaded page to the second) the resource dictionaries that were added as merged resources at the application level seem to be lost and we get an error when trying to access a keyed resource.

## Simple MAUI Binding Issue

The `simple-maui-binding-issue` directory contains the solution illustrating a repro of a truly odd bug impacting embedded MAUI in iOS when using a ControlTemplate applied to all the MAUI pages, such as for ensuring every page displays a custom navigation bar.

The bug is none of the bindings on the content page loaded in the content presenter of the template work the first time we navigate from one MAUI page to the next if any kind of modal controller was displayed in iOS first (alert, action sheet, etc.).

So take the navigation pattern of Native iOS Controller -> Maui Page One -> Maui Page Two. Now, presume that before navigating to Maui Page One an alert was displayed communicating we were loading some data before navigating.
In this scenario, the bindings on MAUI Page One work OK. When then navigating to MAUI Page Two, none of the bindings will work on MAUI Page Two - no data is displayed, no commands work on tap, etc.
If we navigate back to Maui Page One and then to Maui Page Two, then all the bindings work fine and we see the data on the page we would expect. This ONLY happens on that first navigation to MAUI Page Two and ONLY if some form of 
Modal Dialog controller was displayed first on the Native iOS controller.

## Simple MAUI Core

The `simple-maui-core` directory contains just a simple project to house all the MAUI pages, view models, etc. so they could be shared between the single project embedded example and the separate .NET Android example.
This is in line with our real world use of a shared project for the MAUI code to be referenced by the individual Android and iOS projects.



