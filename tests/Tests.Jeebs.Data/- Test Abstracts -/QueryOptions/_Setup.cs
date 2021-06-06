// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using NSubstitute.Extensions;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public abstract class QueryOptions_Tests<TOptions, TBuilder, TId>
		where TOptions : QueryOptions<TId>
		where TBuilder : class, IQueryPartsBuilder<TId>
		where TId : StrongId, new()
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
				Maximum = F.Rnd.Lng,
				Skip = F.Rnd.Lng
			};

			var builder = Substitute.For<TBuilder>();
			builder.Create<TestModel>(Arg.Any<long?>(), Arg.Any<long>()).Returns(parts);
			builder.ReturnsForAll(parts.Return());

			return builder;
		}
	}
}
