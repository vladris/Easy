Easy
====

A library that makes developing for Windows 8 easy (well, easier).

The code is being refactored out of the Windows 8 app I wrote, called
[WriteRT](http://vladris.com/writert).

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

License
-------

Copyright (c) 2013 Vlad Riscutia

[Microsoft Public License](http://opensource.org/licenses/ms-pl)
