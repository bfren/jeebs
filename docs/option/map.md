---
layout: default
title: Map
parent: Option<T> Type
nav_order: 2
---

# Map
{: .no_toc }

## Contents
{: .no_toc }

- TOC
{:toc}

## Functional or fluent?

Once we have used `Return<T>(T) -> Option<T>` to lift a value into the world of `Option<T>`, we need to do something with it by chaining our functions together.  The `Option<T>` class comes with a load of methods which are actually wrappers for the functions that actually do the work.

So, although you can use the functions directly, you'll find it much more convenient to write chains using the common C# fluent syntax.  These two are functionally identical:

```csharp
var option = Return(42);

// functional syntax
var functional = F.OptionF.Map(option, x => x.ToString(), DefaultHandler);

// fluent syntax
var fluent = option.Map(x => x.ToString(), DefaultHandler);
```

In that snippet, `functional` and `fluent` are both `Some<string>` with Value `"42"`.

From now on, I will give function signatures as their equivalent method on `Option<T>` because that *should* help keep things a little clearer!

## `Map<U>(Func<T, U>, Handler) -> Option<U>`

`Map` does a `switch` and behaves like this:

- if the input `Option<T>` is `None<T>`, return `None<U>` with the original Reason
- if the input `Option<T>` is `Some<T>`...
  - get the Value `T`
  - use that as the input and execute `Func<T, U>`, then wrap the result in `Some<U>`
  - catch any exceptions using `Handler`

See it in action here:

```csharp
var result =
    Return(3)
        .Map(
            x => x * 4,
            DefaultHandler
        )
        .Map(
            x => x / 2,
            e => new Msg.DivisionFailedMsg(e)
        )
        .Map(
            x => x * 7,
            DefaultHandler
        )
        .Map(
            x => $"The answer is {x}.",
            DefaultHandler
        )
        .AuditSwitch(
            some: x => Console.Write(x),
            none: x => Console.Write(x)
        );

public static class Msg
{
    public sealed record DivisionFailedMsg(Exception Exception) : IExceptionMsg { }
}
```

Note the `AuditSwitch(Action<T>, Action<IMsg>)` in there - the `Audit` methods pass the current `Option<T>` appropriately and then return it without altering it in any way - mostly useful for logging.

If you change the second `Map` in that sample so you have `x / 0` instead of `x / 2` and re-run the snippet, you will see that you still end up with an `Option<string>`, only this time it's `None<string>` with a `DivisionFailedMsg` as the Reason.

## Next

Similar to Map we have [Bind](bind) ...
