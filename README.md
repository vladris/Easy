Easy
====

A library that makes developing for WinRT easy (well, easier).

The code is being currently used in [WriteRT](http://vladris.com/writert),
a creative writing Windows Store app.

Get it from [NuGet](https://www.nuget.org/packages/Easy/):

```
PM> Install-Package Easy
```

Classes
-------

### Easy.IO.AutoSave<T>

Abstract base class for auto-save functionality, wraps a timer that
periodically performs a save based on the given `ISaveProvider` (below).

### Easy.IO.AutoSaveString

Specialization of `AutoSave<T>` for an `ISaveProvider<string>`, enables
auto-save functionality.

```csharp
// Initialize an auto-save every 10 seconds
var autoSave = new AutoSaveString(provider, TimeSpan.FromSeconds(10));

// Starts atuo-saving every 10 seconds
autoSave.Start();

...

// Stops auto-saving
autoSave.Stop();
```

### Easy.IO.AutoSaveBytes

Specialization of `AutoSave<T>` for an `ISaveProvider<bytes[]>`, similar
to the `AutoSaveString` above.

### Easy.IO.File

Can create unique temporary files and perform transactional file saves.

```csharp
// Temporary file with guaranteed unique name
IStorageFile temp = await File.CreateTemporaryFileAsync();

// Transactional file save
await File.SaveAsync(temp, "some content");
```

### Easy.IO.ISaveProvider<T>

A generic interface which provides an `IStorageFile` object as the
`DestinationFile` property and data to be saved of type `T` as the `Data`
property. Classes implementing this interface would encapsulate all data
required to perform a file save (the file to save to and the data to be
saved).  

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

### Easy.Text.Highlight.BaseHighlighter

Abstract class from which highlighters should derive. Deriving classes
need to implement the `DefaultFormatting` method which should reset
formatting to (non-highlighted) default and the `AddHighlightRules` which
should contain a list of calls to `AddHighligtRule`, mapping regular 
expressions to formatting actions.

Look at `Easy.Text.Highlight.Markdown` for a sample implementation.

Note highlighting can only run on the UI thread because the 
`ITextDocument` and other involved WinRT objects cannot be used on other 
threads.

### Easy.Text.Highlight.Markdown

Provides Markdown highlighting for a given `ITextDocument`.

```csharp
var highlighter = new Markdown(SomeTextDocument);

// Applies Markdown formatting on the given text
highlighter.Highlight();
```

### Easy.Text.Search

Enables string search on an `ITextDocument`, selecting the found string.

```csharp
var search = new Search(SomeTextDocument);

// Highlights the first instance of 'foo' in the text
search.FindFirst("foo"); 

// Highlights the next instance of 'bar' after the previously found 'foo'
search.FindNext("bar"); 

...

// Search wraps around to the beginning of the text
search.FindNext("bar"); 
```

### Easy.Text.WordCount

Provides asynchronous word count on an `ITextDocument`.

```csharp
var wordCount = new WordCount(SomeTextDocument);

int words = await wordCount.Count(); 
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

Copyright (c) 2013-2014 Vlad Riscutia

[Microsoft Public License](http://opensource.org/licenses/ms-pl)
