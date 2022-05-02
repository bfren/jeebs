// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Query;
using NSubstitute.Extensions;
using StrongId;

namespace Jeebs.Data.Testing.Query;

public static partial class FluentQueryHelper
{
	/// <summary>
	/// Create an <see cref="IFluentQuery{TEntity, TId}"/> substitute setup to work fluently
	/// </summary>
	/// <typeparam name="TEntity">Entity Type</typeparam>
	/// <typeparam name="TId">Entity ID Type</typeparam>
	public static IFluentQuery<TEntity, TId> CreateSubstitute<TEntity, TId>()
		where TEntity : IWithId<TId>
		where TId : class, IStrongId, new()
	{
		var fluent = Substitute.For<IFluentQuery<TEntity, TId>>();
		fluent.ReturnsForAll(fluent);

		return fluent;
	}
}
