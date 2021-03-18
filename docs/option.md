---
layout: default
title: Option<T> Type
nav_order: 1
---

## The Option Type - Why?

F# contains a built-in `Option` type, and I wanted to be able to code in that style but using C#.  Over a year or so I have designed `Option<T>` to mimic some of F#'s behaviour using C# way.  It works best when you chain pure (and small) functions together - and if you use it well your exception handler will have very little to do!

The other inspiration behind the type is Scott Wlaschin's [Railway Oriented Programming](https://fsharpforfunandprofit.com/posts/against-railway-oriented-programming/).  I have limited experience of functional programming, so my `Option<T>` is not an implementation of his work, but it contains some similar ideas: in particular the 'failure' or 'sad' path, whereby if one function fails, the rest are skipped over.

## Concepts

The following serves as an introduction to the concepts behind `Option<T>` which are likely to be a little alien to most C# developers.  However, if you want to skip ahead straight to the code, feel free!

### Function Signatures

As `Option<T>` is a mix of C# and F# I will be using the following notation across the documentation: `function signature -> return type`, for example `AddTwoNumbers(int, int) -> int`.  This is partly for brevity, and partly because it is similar to how function signatures are written in F#, which will become useful as the functions get more complex.

### Pure Functions

'Pure' functions have no effect outside themselves.  In other words, they receive an input, act on it, and return an output.  They don't affect state, and they don't affect the input object.

In the OO world of C#, I'll admit, this is odd.  There's not really any such thing as a 'function' - at least not one that exists outside a class definition.  However as far as I can, `Option<T>` is written as a series of pure functions, so even the methods in the `Option<T>` class and the extension methods are simply wrappers for the functions, which all live in the `F.OptionF` namespace.

### `Some` and `None`

`Option<T>` is an abstract class with two implementations.  The return type for a function is *always* `Option<T>`, but the actual object will be one of these:

- `Some<T>` which contains a `Value` property of type `T`
- `None<T>` which contains a `Reason` property of type `IMsg?`

Think of `None<T>` as a more useful nullable, because it comes with a reason *why* it has no value.

### No More Exceptions

Within the Jeebs library - and I encourage you to follow the same discipline if you decide to you `Option<T>`, the contract is that **if a function returns `Option<T>` it has handled all its exceptions so you don't have to**.

This is a critical part of the usefulness of `Option<T>`, and to be honest if you prefer having and handling exceptions I suggest you stop reading!  However, I do believe there is a better way...

### The World of Options

F# developers like to talk about *monads*, which we have in C# too: `IEnumerable<T>` is a monad, for example.  Effectively, monads are a wrapper type with certain properties - if you want to know more, there are better teachers than me!

**You cannot create an `Option<T>` - whether `Some<T>` or `None<T>` directly.**  Instead there are wrapper functions that do the creating for you, and assist with exception handling.

So, let's begin with them!

## Return `Some<T>`

### `Return<T>(T) -> Option<T>`

The following snippets all do the same thing, which is wrap a value in `Some<T>` (you should be able to run this code in C# interactive):

```csharp
int number = 42;

# use a pure function to return a value as an Option<T>
Option<int> option = F.OptionF.Return(number);

# use implicit operator - which calls F.OptionF.Return
Option<int> option = number;

# use an extension method - which calls F.OptionF.Return
Option<int> option = number.Return();
```

In each case the variable `option` is of type `Option<int>`, but specifically `Some<int>`, which means you can do the following:

```csharp
if (option is Some<int> number)
{
    Console.Write("The answer to the question is '{0}'.", number.Value);
}

# The answer to the question is '42'.
```

Although the function signature of `Return<T>(T)` is `Option<T>`, in reality it *always* returns `Some<T>` - except when it doesn't, which is if you try to wrap a null value in `Some<T>`.  You can do this if you really want (though I'm not sure why) by using `Return<T?>(T?, bool) -> Option<T>` - if you set the `allowNull` boolean to `true` there, you will get a `Some<T?>` where the Value is `null`.

### `Return<T>(Func<T>, Handler) -> Option<T>`

Sometimes you may not have the value to hand, but you do have a function that returns a value, which you want to lift into the world of `Option<T>`.  You can do this by using `Return<T>(Func<T>, Handler)` - **but here be dragons**... The first one of these works fine, but the second one has a problem:

```csharp
var number = 42;
var works = F.OptionF.Return(42); # returns Some<int>
var fails = F.OptionF.Return(() => 42 / 0); # we have a problem!
```

When the second is run, there would be an Exception - a `DivideByZeroException` to be precise.  But a key principle of `Option<T>` is that we **always** handle our exceptions.  This is where the delegate `F.OptionF.Handler` comes in, well, handy.

Here is the signature of the delegate:

```csharp
public delegate IExceptionMsg Handler(Exception e);
```

This handler takes an `Exception` and returns an `IExceptionMsg`.  `Return<T>(Func<T>, Handler)` creates a `None<T>` and adds that message as the Reason, like so:

```csharp
int number = 42;
Option<int> option = F.OptionF.Return(
    () => number / 0,
    e => new Msg.TriedToDivideByZeroMsg()
);

if (option is None<T> failure)
{
    Console.Write("Failed when {0}.", failure.Reason);
}

# Failed when Msg.TriedToDivideByZero.
```

## Return `None<T>`

Most of the time you won't be creating `None<T>` values yourself - you will be wrapping functions and values in `Return<T>` and `Map<T>` (which we'll come to in a moment).

However, sometimes you might want to do a check rather than wait for the Exception to be caught - in which case you'll need `F.OptionF.None<T>(IMsg)`.

## Map

### `Map<T, U>(Option<T>, Func<T, U>, Handler) -> Option<U>`

## Messages
