# CelesteMod.Templates
Templates for Celeste code mods!

[![NuGet](https://img.shields.io/nuget/v/CelesteMod.Templates)](https://www.nuget.org/packages/CelesteMod.Templates/)

Prerequisites:

* Celeste
* [Everest](https://everestapi.github.io)
* [.NET SDK](https://dotnet.microsoft.com/en-us/download)
* A C# IDE (Visual Studio, Rider, Code, Nano, etc.)

Installation:

`dotnet new --install CelesteMod.Templates`

From your Celeste/Mods directory:

```shell
mkdir MyCoolMod
cd MyCoolMod
dotnet new celestemod
```

This will create a solution for your mod and a series of classes in the namespace `Celeste.Mod.MyCoolMod` ready for you to start developing.

Available parameters:
* `--Samples`: Creates a set of sample entities and triggers, including Ahorn and Lönn plugins. (Defaults to false)
* `--Exports`: Includes a static class for use with `MonoMod.ModInterop`. (Defaults to false)
* `--Hooks` : Generates a series of helper methods for loading and unloading hooks on level load rather than just on mod load. (Defaults to false)
* `--Settings` : Includes a blank `EverestModuleSettings` class and configures the module to look for it. (Defaults to true)
* `--Session` : Includes a blank `EverestModuleSession` class and configures the module to look for it. (Defaults to true)
* `--Logging` : Sets logging level to `Info` specifically for release builds instead of the default `Verbose`. (Defaults to true)
* `--GitHub` : Generates a GitHub action for building your mod. (Defaults to false)
* `--Core` : Generates a .NET Core mod - this requires .NET Core Everest (**breaking compatility with the current Everest stable/beta/dev versions as of now**), but offers access to .NET 7.0 features. (Defaults to false)