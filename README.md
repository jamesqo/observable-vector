# Observable Vector

A drop-in implementation of IObservableVector for Windows 10 apps.

## What is this?

If you're a Windows 10 developer, you're probably familiar with MVVM, or the *Model-View-ViewModel* pattern. For those who aren't, it's essentially a design pattern (based on binding and events) that helps keep your code maintainable by separating the UI code from the "business logic."

The [IObservableVector](https://msdn.microsoft.com/en-us/library/windows/apps/br226052.aspx) interface is one of the facilities Microsoft provides to help you implement this pattern in your app. Basically, it's just like a normal List, but outputs an event whenever it's modified. This lets you bind to its properties in XAML like `{Binding MyAwesomeList.Count}` without having to worry what goes on behind the scenes.

## So... what is this?

Unforunately, Microsoft doesn't provide a default implementation of `IObservableVector`. This means that normally, you wouldn't be able to use it in your app without writing a lot of boilerplate code.

Observable Vector simply provides a class that implements IObservableVector, so you don't have to make one yourself.

## OK, great. How do I install this?

Just:

```powershell
Install-Package ObservableVector
```

If you want the non-generic version ([see below](#why-would-i-want-to-use-the-non-generic-version) for why you might), then type this instead:

```powershell
Install-Package ObservableVector.NonGeneric
```

# Getting Started

TBC.

# FAQ

## Why would I want to use the non-generic version?

TBC.
