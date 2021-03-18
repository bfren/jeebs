---
layout: default
title: Option<T> Type
nav_order: 1
has_children: true
---

# The `Option<T>` Type
{: .no_toc }

## Contents
{: .no_toc }

- TOC
{:toc}

## The world of `Option<T>`

What if you could get away from huge classes with monster methods?  What if you didn't have to worry about handling null values?

What if you could wrap values in a consistent way, have helpful feedback when something goes wrong, and chain small reusable testable reliable functions to provide stable functionality?

You can: in the world of `Option<T>`!

## What can it do?

To whet your appetite for what using `Option<T>` enables you to write, check out the following snippets - you'll notice that `Option<T>` supports mixing sync and async functions.

The first is from my website's `BlogController`.  Each of the `GetXXX(query)` methods returns `Option<T>`, which are joined together via a Linq `SelectMany`, and used to build the model for the List view.

```csharp
ProcessOptionAsync(
    from posts in GetPostsAsync(query)
    from title in GetTitle(query)
    from sidebar in GetSidebarAsync(query)
    select new ListModel
    {
        Query = query,
        Title = title,
        Section = Section,
        Page = Page,
        Posts = posts,
        Sidebar = sidebar
    },
    m => View("List", m)
);
```

Or this, which builds a SQL query, executes it, and then processes the results:

```csharp
Return(modify)
    .Map(
        x => GetPostsQuery<TModel>(x),
        e => new Msg.ErrorGettingPostsQueryExceptionMsg(e)
    )
    .BindAsync(
        x => x.ExecuteQueryAsync()
    )
    .BindAsync(
        x => Process<List<TModel>, TModel>(x, filters)
    );
```

In both situations there *cannot* be an unhandled exception, there *cannot* be any `null` values, and if something does go wrong an object with a helpful error message is return.

## Key principles

In the world of `Option<T>` the following key principles are followed:

- the return type is always `Option<T>` (rather than `Some<T>` or `None<T>`)
- if a function returns `Option<T>` it means all exceptions have been handled
- when returning `None<T>` we must give a reason
- everything that can go wrong in your system has an `IMsg` which describes it

If you haven't done any functional programming I suggest you read through the following, but if you want to see what `Option<T>` can do, you can go straight to [Return](return).

## Why?

F# contains a built-in `Option` type, and I wanted to be able to code in that style but using C#.  Over a year or so I have designed `Option<T>` to mimic some of F#'s behaviour using C# way.  It works best when you chain pure (and small) functions together - and if you use it well your exception handler will have very little to do!

The other inspiration behind the type is Scott Wlaschin's [Railway Oriented Programming](https://fsharpforfunandprofit.com/posts/against-railway-oriented-programming/).  I have limited experience of functional programming, so my `Option<T>` is not an implementation of his work, but it contains some similar ideas: in particular the 'failure' or 'sad' path, whereby if one function fails, the rest are skipped over.

## Concepts

The following serves as an introduction to the concepts behind `Option<T>` which are likely to be a little alien to most C# developers.  However, if you want to skip ahead straight to the code, feel free!

### Namespaces

`Option<T>` and all its extension methods live in the `Jeebs` namespace (and some useful features in `Jeebs.Linq`).  However the actual 'pure' functions live in `F.OptionF` - I put all my functional-style code in the `F` namespace, and add an `F` suffix to the class type.  My preferred style is then to add `using static F.OptionF` so the functions can be accessed directly.

### Pure functions

'Pure' functions have no effect outside themselves.  In other words, they receive an input, act on it, and return an output.  They don't affect state, and they don't affect the input object.

In the OO world of C#, I'll admit, this is odd.  There's not really any such thing as a 'function' - at least not one that exists outside a class definition.  However as far as I can, `Option<T>` is written as a series of pure functions, so even the methods in the `Option<T>` class and the extension methods are simply wrappers for the functions, which all live in the `F.OptionF` namespace.

### `Some<T>` and `None<T>`

`Option<T>` is an abstract class with two implementations.  The return type for a function is *always* `Option<T>`, but the actual object will be one of these:

- `Some<T>` which contains a `Value` property of type `T`
- `None<T>` which contains a `Reason` property of type `IMsg?`

Think of `None<T>` as a more useful nullable, because it comes with a reason *why* it has no value.

### No More Exceptions

Within the Jeebs library - and I encourage you to follow the same discipline if you decide to you `Option<T>`, the contract is that **if a function returns `Option<T>` it has handled all its exceptions so you don't have to**.

This is a critical part of the usefulness of `Option<T>`, and to be honest if you prefer having and handling exceptions I suggest you stop reading!  However, I do believe there is a better way...

  To enter the world we need to 'lift' values into it so we can benefit from all its features.

## Function signatures

As `Option<T>` is a mix of OO and functional programming styles I will be using the following notation across the documentation: `function signature -> return type`, for example `AddTwoNumbers(int, int) -> int`.  This is partly for brevity, and partly because it is similar to how function signatures are written in F#, which will become useful as the functions get more complex.

## Next
{: .no_toc }

So, let's begin with [Return](return)!
