Easy <sup>α</sup> 
====

A library that makes developing for WinRT easy (well, easier).

The code is being refactored out of the Windows 8 app I wrote, called
[WriteRT](http://vladris.com/writert).

Currently in early alpha, use at your own risk.

Classes
-------

### Easy.Platform.Setting

A setting with a default value in case its key is not in the data container.

```csharp
// Local "foo" setting, default value is 0
var foo = Setting<int>.Local("foo");

// Roaming "bar" setting, default value is "baz"
var bar = Setting<string>.Roaming("bar", "baz");

foo.Value = 42;  // setting associated with foo is now 42
string x = bar.Value; // x is now "baz"
```

### Easy.UI.Toast

Easily display toast messages. Currently supports text or title + text.
Don't forget to mark your app as toast capable in its manifest.

```csharp
Toast.Show("A toast!");

Toast.Show("Title", "Some content");
```

License
-------

Copyright (c) 2013 Vlad Riscutia

[Microsoft Public License](http://opensource.org/licenses/ms-pl)
