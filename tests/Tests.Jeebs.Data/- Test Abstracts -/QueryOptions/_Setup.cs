// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using NSubstitute.Extensions;

namespace Jeebs.Data.Query.QueryOptions_Tests;

public abstract class QueryOptions_Tests<TOptions, TBuilder, TId>
	where TOptions : QueryOptions<TId>
	where TBuilder : class, IQueryPartsBuilder<TId>
	where TId : class, IUnion, new()
{
	protected abstract (TOptions options, TBuilder builder) Setup();

	protected QueryParts Qp =>
		Arg.Any<QueryParts>();

	protected virtual TBuilder GetConfiguredBuilder(ITable table)
	{
		var parts = new QueryParts(table)
		{
			Maximum = Rnd.UInt64,
			Skip = Rnd.UInt64
		};

		var builder = Substitute.For<TBuilder>();
		builder.Create<TestModel>(Arg.Any<ulong?>(), Arg.Any<ulong>()).Returns(parts);
		builder.ReturnsForAll(R.Wrap(parts));

		return builder;
	}
}
