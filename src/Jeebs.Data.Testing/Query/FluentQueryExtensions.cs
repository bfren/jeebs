// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Query;
using NSubstitute.Core;
using Xunit.Sdk;

namespace Jeebs.Data.Testing.Query;

/// <summary>
/// <see cref="IFluentQuery{TEntity, TId}"/> extension methods.
/// </summary>
public static class FluentQueryExtensions
{
	/// <summary>
	/// Assert calls to an <see cref="IFluentQuery{TEntity, TId}"/> object.
	/// </summary>
	/// <typeparam name="TEntity">Entity Type.</typeparam>
	/// <typeparam name="TId">Entity ID Type.</typeparam>
	/// <param name="this">Fluent Query object.</param>
	/// <param name="inspectors">Call inspectors.</param>
	/// <exception cref="CollectionException"></exception>
	public static void AssertCalls<TEntity, TId>(this IFluentQuery<TEntity, TId> @this, params Action<ICall>[] inspectors)
		where TEntity : IWithId
		where TId : class, IUnion, new() =>
		FluentQueryHelper.AssertCollection([.. @this.ReceivedCalls()], inspectors);
}
