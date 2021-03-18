---
layout: default
title: Option Chains
parent: Option<T> Type
nav_order: 1
---

# Option Chains
{: .no_toc }

## Contents
{: .no_toc }

- TOC
{:toc}

## Isn't this an over-complicated mess?

It's all very well creating a lovely `Option<T>` with `Return<T>(T) -> Option<T>`, but what do you do with it?  Do you really want to have code like this everywhere:

```csharp
var option = Return(42);
if (option is None<int> none)
{
    // do something
}
else if (option is Some<int> some)
{
    // do something else
}
```

Of course not!  That's a mess.

## Principles

Instead we use functions to chain `Option<T>` results together, so the flow looks something like this:

```plaintext
Return<A> ->> Link<A, B> ->> Link<B, C> ->> Link<C, D> ->> Some<D>
```

If all the links work, each receives value A / B / C, returns value B / C / D, and we end up with `Some<D>`.  This is the basic concept behind Scott Wlaschin's [Railway Oriented Programming](https://fsharpforfunandprofit.com/posts/against-railway-oriented-programming/).  It's a different way of coding to OO / C#, because instead of writing a method that does a lot of things, we write several small functions, each of which does one thing, and then we chain them together.  (In F# this is called composition, but we can't do that in C# so instead we use a 'fluent' style syntax.)

This is great - but what if something goes wrong?  Then we want the flow to look something like this:

```plaintext
Return<A> ->> Link<A, B> ->> Link<B, C> ->> Link<C, D>
                       \
                        \
                         None<B> - - - - - - - - - - - ->> None<D>
```

Something goes wrong in the first link, and instead of a `Some<B>` to pass a value to the next link in the chain, we get a `None<B>` (with a Reason of course).  All the next links in the chain are skipped, the `None<B>` is transformed into a `None<C>` and then to a `None<D>` (preserving the original Reason message), and returned.

The chaining is handled by two function groups: `Map` and `Bind`.  These do the switching for you:

- if the input is `Some<T>` they pass the value to your function
- if the input is `None<T>` they skip your function and return `None<U>` (the next type), preserving the Reason message

## Next

So let's look at [Map](map) first of all...
