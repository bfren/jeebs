// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using NSubstitute.Extensions;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public abstract class QueryPartsBuilder_Tests
	{

#pragma warning disable NS1004 // Argument matcher used with a non-virtual member of a class.
		protected QueryParts Qp =>
			Arg.Any<QueryParts>();
#pragma warning restore NS1004 // Argument matcher used with a non-virtual member of a class.

		public static (TestBuilder builder, Vars v) Setup()
		{
			var table = Substitute.For<ITable>();

			var parts = new QueryParts(table);

			var columns = Substitute.For<IColumnList>();

			var idColumn = Substitute.For<IColumn>();

			var builder = Substitute.ForPartsOf<TestBuilder>();
			builder.Table.Returns(table);
			builder.IdColumn.Returns(idColumn);
			builder.Configure().GetColumns<TestEntity>().Returns(columns);

			return (builder, new(table, columns, idColumn, parts));
		}

		public sealed record Vars(
			ITable Table,
			IColumnList Columns,
			IColumn IdColumn,
			QueryParts Parts
		);
	}

	public record TestId(long Value) : StrongId(Value)
	{
		public TestId() : this(0) { }
	}

	public record TestEntity(TestId Id, int Foo, bool Bar) : IWithId<TestId>;

	public record TestModel(int Foo);

	public abstract class TestBuilder : QueryPartsBuilder<TestId> { }
}
