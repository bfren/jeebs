---
layout: default
title: Bind
parent: Option<T> Type
nav_order: 3
---

# Bind
{: .no_toc }

## Contents
{: .no_toc }

- TOC
{:toc}

## What's the difference?

Like `Map`, the `Bind` functions receive the value of the input `Option<T>` if it's a `Some<T>`, and are bypassed if it's a `None<T>`.

However, there are two important differences:

1. `Bind` functions return an `Option<T>` (`Map` functions return `T` which is wrapped by `Return`).
2. Therefore they are expected to handle their own exceptions - one of our key principles is the contract that **if a function returns `Option<T>` its exceptions have been handled**.  (The method signature therefore does not have a `Handler` in it.)

That said, you can be naughty because your binding function is still wrapped in a `try..catch` block.  However *all* exceptions caught in that block will return `None<T>` with a `Msg.UnhandledExceptionMsg` as the Reason.

## `Bind<U>(Func<T, Option<U>>) -> Option<U>`

`Bind` does a `switch` and behaves like this:

- if the input `Option<T>` is `None<T>`, return `None<U>` with the original Reason
- if the input `Option<T>` is `Some<T>`...
  - get the Value `T`
  - use that as the input and execute `Func<T, Option<U>>`, then return the result
  - catch any unhandled exceptions using `DefaultHandler`

See it in action here:

```csharp
var result =
    Return(3)
        .Bind( // x is 3
            x => Return(x * 4)
        )
        .Bind( // x is 12
            x => Return(x / 2)
        )
        .Bind( // x is 6
            x => Return(x * 7)
        )
        .Bind( // x is 42
            x => Return($"The answer is {x}.")
        )
        .AuditSwitch(
            some: val => Console.Write(val),
            none: msg => Console.Write(msg)
        );
```

This is identical to the previous snippet at the end of the [Map](map) section - except there are no exception handling options.  So, if you change `x / 2` to `x / 0` you will get an `UnhandledExceptionMsg`.

Bind comes into its own in more complex scenarios, for example data access:

```csharp
Return(customerId)
    .Bind(
        id => db.GetCustomer(id)
    )
    .Bind(
        customer => db.InsertCustomerOrder(customer, order)
    )
    .Bind(
        orderId => db.GetOrderById(orderId)
    )
    .AuditSwitch(
        some: order => mailer.SendOrderConfirmationEmail(order),
        none: reason => notifier.OrderFailed(reason)
    )
    .Map(
        order => new OrderPlacedModel
        {
            Id = order.Id,
            Products = order.Products
        },
        DefaultHandler
    );

// result is Option<OrderPlacedModel>
```

If `db.GetCustomer()` fails, the next two operations are skipped, `AuditSwitch` is run with the `none` option, and a `None<OrderPlacedModel>` is returned with the reason from `db.GetCustomer()`.

You can of course also return Tuples instead of single values, and C# is clever enough to give Tuples automatic names now as well:

```csharp
Return(customerId)
    .Bind(
        id => db.GetCustomer(id)
    )
    .Bind(
        customer =>
        {
            var orderId = db.InsertCustomerOrder(customer, order);
            return (customer, orderId)
        }
    )
    .Bind(
        x =>
        {
            var order = db.GetOrderById(x.orderId);
            return (customer, order);
        }
    );

// result is Option<(CustomerEntity, OrderEntity)>
```

## Adding it all together

In the next section we will actually make a simple test app with everything you need to play around with `Map` and `Bind`.  Before then here is one final snippet from `Jeebs.WordPress` to show a real example (simplified a bit and minus the huge SQL query) of what this looks like in practice:

```csharp
internal async Task<Option<List<T>>> GetTaxonomyAsync<T>(Taxonomy taxonomy)
    where T : ITermWithRealCount
{
    using var w = Db.UnitOfWork;

    return await
        Return(
            (Db, w, taxonomy)
        )
        .Map(
            GetQuery,
            e => new Msg.GetTaxonomyQueryExceptionMsg(e)
        )
        .BindAsync(
            ExecuteQuery
        )
        .MapAsync(
            x => x.ToList(),
            DefaultHandler
        );
}

internal static (IUnitOfWork, Taxonomy, string) GetQuery((IWpDb, IUnitOfWork, Taxonomy) input)
{
    var (db, w, taxonomy) = input;
    return (w, taxonomy,
        $"SELECT * FROM {db.TaxonomyTable}......"
    );
}

internal static Task<Option<IEnumerable<T>>> ExecuteQuery((IUnitOfWork, Taxonomy, string) input)
{
    var (w, taxonomy, sql) = input;
    return w.QueryAsync<T>(sql, new { taxonomy });
}
```

Here we have two potentially problem-ridden methods - returning a complex SQL query from a given input and executing a database query - that are now **pure functions**: notice they are declared `static`.  This is best practice, think of it like Dependency Injection but *functional*.  The functions require no state, and interact with nothing else, so given the same input they will *always* return the same output.

This means they can be tested easily, and once they work correctly they will *always* work in the same way so you can rely on them.

You will also notice the use of the `-Async` variations of `Map` and `Bind` - we will come to these later, but for now note that although `x.ToList()` is not doing anything asynchronously, because the *input* is a `Task<Option<T>>` we must use the `-Async` variant.  This leads to another of our key principles: **once we are in the async world we stay in the async world**.

## Next

Let's try a [Sample Application](sample-application) ...
