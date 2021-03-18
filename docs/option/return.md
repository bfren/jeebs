---
layout: default
title: Return and None
parent: Option<T> Type
nav_order: 0
---

# Return and None
{: .no_toc }

## Contents
{: .no_toc }

- TOC
{:toc}

## `Return<T>(T) -> Option<T>`

To lift a value into the world of `Option<T>` the main functions we use are all called **Return**.

The following snippets all do the same thing, which is wrap a value in `Some<T>` (you should be able to run this code in C# interactive):

```csharp
int number = 42;

// use a pure function to return a value as an Option<T>
Option<int> option = F.OptionF.Return(number);

// use the same function, but in my preferred style
using static F.OptionF;
Option<int> option = Return(value);

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

This is nice and simple, but what if your value comes from another function?  Wouldn't it be nice if you could pass a `Func<T>` instead of a `T`, and wouldn't it be nice if it caught any exceptions for you, so don't have to wrap it in a `try..catch` block?

Yes, it would.

But first, we need to explore how we return `None<T>`

## `None<T>(IMsg) -> Option<T>`

Where `Some<T>` wraps a value, `None<T>` is a null-safe way of representing no value, with a helpful reason *why* there is no value: some input was invalid, an exception occurred, and so on.  Note that **we *always* need to give a reason for a `None<T>`** - another key principle in the world of `Option<T>`.

Let's dive right in!

```csharp
var none = Divide(42, 0);
if (none is None<int> failed)
{
    Console.WriteLine("Something went wrong: {0}.", failed.Reason);
}

var some = Divide(42, 6);
if (some is Some<int> succeeded)
{
    Console.WriteLine("The result is: {0}.", succeeded.Value);
}

// Something went wrong: YouCannotDivideByZeroMsg { }.
// The result is: 7.

Option<int> Divide(int x, int y)
{
    if (y == 0)
    {
        return None<int>(new Msg.YouCannotDivideByZeroMsg());
    }

    return x / y;
}

public static class Msg
{
    public sealed record YouCannotDivideByZeroMsg : IMsg { }
}
```

My pattern is for each class I write to have a `Msg` static class, which contains all the messages relating to that class.  My practice is to implement my messages as `record` types - you don't have to do that, unless you want to use the built-in message types from the `Jeebs` NuGet package.

(If your message has a public parameterless constructor you can use the function `None<T, IMsg>() -> Option<T>` to create your `None<T>`.)

`Jeebs.Option` comes with two message interfaces: `IMsg` (which has no properties or methods) and `IExceptionMsg` (which has one property: the exception).  We'll explore exception handling in the next section - but another way of writing the code block above would be like this:

```csharp
var none = Divide(42, 0);
if (none is None<int> failed)
{
    Console.WriteLine("Something went wrong: {0}.", failed.Reason);
}

var some = Divide(42, 6);
if (some is Some<int> succeeded)
{
    Console.WriteLine("The result is: {0}.", succeeded.Value);
}

// Something went wrong: DivisionExceptionMsg { Exception = ... }.
// The result is: 7.

Option<int> Divide(int x, int y)
{
    try
    {
        return x / y;
    }
    catch (Exception e)
    {
        return None<int>(new Msg.DivisionExceptionMsg(e));
    }
}

public static class Msg
{
    public sealed record DivisionExceptionMsg(Exception Exception) : IExceptionMsg { }
}
```

Now we've seen `IExceptionMsg` we can turn to the *other* Return function, which takes a `Func<T>` instead of a `T`.

## `Return<T>(Func<T>, Handler) -> Option<T>`

A key principle of `Option<T>` is that **we always handle our exceptions**.  Therefore whenever we try to 'lift' a function instead of a value into the world of `Option<T>`, we need to catch things that go wrong.

This is where the delegate `F.OptionF.Handler` comes in, well, handy.  Here is the definition of `Handler`:

```csharp
public delegate IExceptionMsg Handler(Exception e);
```

It takes an `Exception` and returns an `IExceptionMsg`.  The handler is used by `Return<T>(Func<T>, Handler)`, which creates a `None<T>` and adds the reason message created by the handler.

So, this snippet does exactly what the `Divide(int, int) -> Option<int>` function did in the previous example, but without the `try..catch` block:

```csharp
int number = 42;
Option<int> option = Return(
    () => number / 0,
    e => new Msg.DivisionExceptionMsg(e)
);

if (option is None<int> failed)
{
    Console.Write("Failed with {0}.", failed.Reason);
}

// Failed with Msg.DivisionExceptionMsg { Exception = ... }.

public static class Msg
{
    public sealed record DivisionExceptionMsg(Exception Exception) : IExceptionMsg { }
}
```

## Messages

Messages are a simple but incredibly powerful way of describing everything that can go wrong in your system.  You could have your own namespace for them all, but I prefer to define the messages right next to the class that uses them.

To realise the true power of Messages, you need to be disciplined about *never* reusing them (or only rarely - I reuse mine only when I have two versions of the same function, for example sync / async).

This means:

- when you log a `None<T>` with its Reason, you know exactly where the problem occurred
- if you want to provide user feedback and translations, you can have specific error messages based on where the problem occurred

The messages we've used so far have been pretty simple, but here is an example of one from `Jeebs.Data.Mapping`:

```csharp
public sealed record UpdateErrorMsg<T>(string Method, long Id)
    : LogMsg(LogLevel.Warning, "{Method} {Id}")
{
    public override Func<object[]> Args =>
        () => new object[] { Method, Id };
}
```

This message captures various important pieces of information:

- the type `T` is the type of the entity being updated
- `Method` is the name of the update method
- `Id` is the ID of the entity being updated

`UpdateErrorMsg<T>` extends the `LogMsg` abstract record from the `Jeebs` package to set the log level, and provide a custom log message using the update values.  Then in the `Update()` function I can do something like this:

```csharp
return rowsAffected switch
{
    1 =>
        True,

    _ =>
        None<bool>(new Msg.UpdateErrorMsg<T>(nameof(UpdateAsync), entity.Id))
}
```

## True and False

In that last code snippet you may have `True`.  That isn't a typo - there are two properties of `F.OptionF`:

- `F.OptionF.True` which returns `Some<bool>` with Value `true`
- `F.OptionF.False` which returns `Some<bool>` with Value `false`

They are identical to writing `Return(true)` or `Return(false)` - but I like the shorthands.

They exist because you don't want to return a `None<bool>` when something fails - you want to return a `Some<bool>` with a `false` value, so you can continue processing.  This is what `F.OptionF.False` is for (or simply `False` if you have `using static F.OptionF`).

## Next

Next, we will look at [Option Chains](chains) ...
