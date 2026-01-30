// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Common.FluentQuery;
using NSubstitute.Extensions;

namespace Jeebs.Data.Testing.Query;

public static partial class FluentQueryHelper
{
	/// <summary>
	/// Create an <see cref="IFluentQuery{TEntity, TId}"/> substitute setup to work fluently.
	/// </summary>
	/// <typeparam name="TEntity">Entity Type.</typeparam>
	/// <typeparam name="TId">Entity ID Type.</typeparam>
	public static IFluentQuery<FluentQuery<TEntity, TId>, TEntity, TId> CreateSubstitute<TEntity, TId>()
		where TEntity : IWithId
		where TId : class, IUnion, new()
	{
		var fluent = Substitute.For<IFluentQuery<FluentQuery<TEntity, TId>, TEntity, TId>>();
		fluent.ReturnsForAll(fluent);

		return fluent;
	}
}
