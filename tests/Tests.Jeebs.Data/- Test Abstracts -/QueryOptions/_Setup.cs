// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using Jeebs.Id;
using Maybe.Extensions;
using NSubstitute.Extensions;

namespace Jeebs.Data.Query.QueryOptions_Tests;

public abstract class QueryOptions_Tests<TOptions, TBuilder, TId>
	where TOptions : QueryOptions<TId>
	where TBuilder : class, IQueryPartsBuilder<TId>
	where TId : IStrongId, new()
{
	protected abstract (TOptions options, TBuilder builder) Setup();

#pragma warning disable NS1004 // Argument matcher used with a non-virtual member of a class.
	protected QueryParts Qp =>
		Arg.Any<QueryParts>();
#pragma warning restore NS1004 // Argument matcher used with a non-virtual member of a class.

	protected virtual TBuilder GetConfiguredBuilder(ITable table)
	{
		var parts = new QueryParts(table)
		{
			Maximum = Rnd.Ulng,
			Skip = Rnd.Ulng
		};

		var builder = Substitute.For<TBuilder>();
		_ = builder.Create<TestModel>(Arg.Any<ulong?>(), Arg.Any<ulong>()).Returns(parts);
		builder.ReturnsForAll(parts.Some());

		return builder;
	}
}
