using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libVKSdk.a", LinkTarget.Simulator | LinkTarget.ArmV7, ForceLoad = true)]
