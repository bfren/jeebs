// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Data.Mapping;
using NSubstitute;
using static F.OptionF;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public static class QueryOptions_Setup
	{
		public static Vars Get(Func<TestOptions, TestOptions>? opt = null)
		{
			var table = Substitute.For<ITable>();

			var parts = new QueryParts(table);

			var idColumn = Substitute.For<IMappedColumn>();

			var map = Substitute.For<ITableMap>();
			map.Table.Returns(table);
			map.IdColumn.Returns(idColumn);

			var mapper = Substitute.For<IMapper>();
			mapper.GetTableMapFor<TestEntity>().Returns(Return(map));

			var options = new TestOptions(mapper);

			return new(table, map, idColumn, parts, opt?.Invoke(options) ?? options);
		}

		public sealed record Vars(
			ITable Table,
			ITableMap Map,
			IColumn IdColumn,
			QueryParts Parts,
			TestOptions Options
		);

		public sealed record TestId(long Value) : StrongId(Value);

		public sealed record TestEntity(TestId Id, int Foo, bool Bar) : IWithId<TestId>;

		public sealed record TestOptions(IMapper Mapper) : QueryOptions<TestEntity, TestId>(Mapper);
	}

	public sealed record Table0 : ITable
	{
		private readonly string name;

		public string Foo { get; init; }

		public Table0(string name, string foo) =>
			(this.name, Foo) = (name, foo);

		public string GetName() =>
			name;
	}

	public sealed record Table1 : ITable
	{
		private readonly string name;

		public string Bar { get; init; }

		public Table1(string name, string bar) =>
			(this.name, Bar) = (name, bar);

		public string GetName() =>
			name;
	}
}
