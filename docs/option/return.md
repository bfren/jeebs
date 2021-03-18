---
layout: default
title: Return
nav_order: 1
---

# Return

## `Return<T>(T) -> Option<T>`

The following snippets all do the same thing, which is wrap a value in `Some<T>` (you should be able to run this code in C# interactive):

```csharp
int number = 42;

// use a pure function to return a value as an Option<T>
Option<int> option = F.OptionF.Return(number);

// use implicit operator - which calls F.OptionF.Return
Option<int> option = number;

// use an extension method - which calls F.OptionF.Return
Option<int> option = number.Return();
```

In each case the variable `option` is of type `Option<int>`, but specifically `Some<int>`, which means you can do the following:

```csharp
if (option is Some<int> number)
{
    Console.Write("The answer to the question is '{0}'.", number.Value);
}

// The answer to the question is '42'.
```

Although the function signature of `Return<T>(T)` is `Option<T>`, in reality it *always* returns `Some<T>` - except when it doesn't, which is if you try to wrap a null value in `Some<T>`.  You can do this if you really want (though I'm not sure why) by using `Return<T?>(T?, bool) -> Option<T>` - if you set the `allowNull` boolean to `true` there, you will get a `Some<T?>` where the Value is `null`.

## `Return<T>(Func<T>, Handler) -> Option<T>`

Sometimes you may not have the value to hand, but you do have a function that returns a value, which you want to lift into the world of `Option<T>`.  You can do this by using `Return<T>(Func<T>, Handler)` - **but here be dragons**... The first one of these works fine, but the second one has a problem (apart from the fact that it's missing a required parameter):

```csharp
var number = 42;
var works = F.OptionF.Return(number); // returns Some<int>
var fails = F.OptionF.Return(() => number / 0); // we have a problem
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

// Failed when Msg.TriedToDivideByZero.
```

## Returning `None<T>`

Most of the time you won't be creating `None<T>` values yourself - you will be wrapping functions and values in `Return<T>` and `Map<T>` (which we'll come to in a moment).

However, sometimes you might want to do a check rather than wait for the Exception to be caught - in which case you'll need `F.OptionF.None<T>(IMsg)`.
