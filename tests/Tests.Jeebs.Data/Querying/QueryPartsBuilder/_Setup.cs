// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using NSubstitute.Extensions;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public abstract class QueryPartsBuilder_Tests
	{
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

	public abstract record TestBuilder : QueryPartsBuilder<TestId>;

	public record TestTable0 : ITable
	{
		private readonly string name;

		public string Foo { get; init; }

		public TestTable0(string name, string foo) =>
			(this.name, Foo) = (name, foo);

		public string GetName() =>
			name;
	}

	public record TestTable1 : ITable
	{
		private readonly string name;

		public string Bar { get; init; }

		public TestTable1(string name, string bar) =>
			(this.name, Bar) = (name, bar);

		public string GetName() =>
			name;
	}
}
