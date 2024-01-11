## Table of Contents

* [Overview](#overview)
* [Simple MAUI First App](#simple-maui-first-app)
* [Simple MAUI Embedded](#simple-maui-embedded)
* [Simple Android Embedded](#simple-android-embedded)
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

## Simple MAUI Core

The `simple-maui-core` directory contains just a simple project to house all the MAUI pages, view models, etc. so they could be shared between the single project embedded example and the sepatate .NET Android example.
This is in line with our real world use of a shared project for the MAUI code to be referenced by the individual Android and iOS projects.



