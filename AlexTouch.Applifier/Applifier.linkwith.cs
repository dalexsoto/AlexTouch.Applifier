using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("Applifier.a", LinkTarget.ArmV7 | LinkTarget.Simulator, ForceLoad = true, Frameworks = "CoreGraphics")]
