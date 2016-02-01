# Observable Vector

A drop-in implementation of IObservableVector for Windows 10 apps.

## What is this?

If you're a Windows 10 developer, you're probably familiar with MVVM, or the *Model-View-ViewModel* pattern. For those who aren't, it's essentially a design pattern (based on binding and events) that helps keep your code maintainable by separating the UI code from the "business logic."

The [IObservableVector](https://msdn.microsoft.com/en-us/library/windows/apps/br226052.aspx) interface is one of the facilities Microsoft provides to help you implement this pattern in your app. Basically, it's just like a normal List, but outputs an event whenever it's modified. This lets you bind to its properties in XAML like `{Binding MyAwesomeList.Count}` without having to worry what goes on behind the scenes.

## So... what is this?

Unforunately, Microsoft doesn't provide a default implementation of `IObservableVector`. This means that normally, you wouldn't be able to use it in your app without writing a lot of boilerplate code.

**Observable Vector** simply provides a class that implements IObservableVector, so you don't have to make one yourself.

## Features?

- Easy to use
- Easy to extend
- Can be used from other languages, like C++/CX or JavaScript

## OK, sounds great. How do I install this?

Just:

```powershell
Install-Package ObservableVector
```

If you want the non-generic version ([see below](#why-would-i-want-to-use-the-non-generic-version) for why you might), then type this instead:

```powershell
Install-Package ObservableVector.NonGeneric
```

# Getting Started

Just use ObservableVector like a normal List:

```csharp
using System.Collections.Generic;

var vector = new ObservableVector<int>();
vector.Add(1);
vector.Add(2);
if (vector.Remove(3))
    Console.WriteLine("Huh?");
```

Subscribe to the `VectorChanged` event for notifications:

```csharp
vector.VectorChanged += MyHandler;

private void MyHandler(IObservableVector<int> sender, IVectorChangedEventArgs e)
{
    bool removed = e.CollectionChange == CollectionChange.ItemRemoved;

    Console.WriteLine("Index of change: {0}", e.Index);
    Console.WriteLine("Was an item removed? {0}", removed);
}
```

You can also subclass it easily:

```csharp
public class MyVector<T> : ObservableVector<T>
{
    protected override void ClearItems()
    {
        base.ClearItems();
        Console.WriteLine("Windows 10 devs rock!");
    }
}
```

# FAQ

## Why would I want to use the non-generic version?

If you're writing your app in a non-.NET language, such as C++/CX or JavaScript, then you'll need to use the non-generic version. This is due to technical limitations of the WinRT platform, which mandates that all exposed types must be sealed and non-generic. See [here](http://stackoverflow.com/questions/9509099/winrt-reason-for-disallowing-custom-generic-types-or-interfaces) for more info.

You may also want to go down this route if you're 1) writing a WinRT component and 2) exposing an ObservableVector from your API, for the reasons mentioned above.

## What's the difference between this and ObservableCollection?

[ObservableCollection](https://msdn.microsoft.com/en-us/library/ms668604(v=vs.110).aspx) signals changes via the INotifyCollectionChanged interface, which is being replaced by IObservableVector. See [this blog post](http://blogs.u2u.be/diederik/post/2012/01/03/Hello-ObservableVector-goodbye-ObservableCollection.aspx) for more.

# License

[MIT](LICENSE)
