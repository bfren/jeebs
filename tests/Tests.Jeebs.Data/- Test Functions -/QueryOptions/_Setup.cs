// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using NSubstitute;
using static F.OptionF;

namespace F.DataF_Tests
{
	public static partial class QueryOptionsF_Tests
	{
		public static (TOptions, Vars) Setup<TOptions, TId>(Func<IMapper, TOptions> create, Func<TOptions, TOptions>? opt = null)
			where TOptions : QueryOptions<TId>
			where TId : StrongId
		{
			var table = Substitute.For<ITable>();

			var parts = new QueryParts(table);

			var idColumn = Substitute.For<IMappedColumn>();

			var map = Substitute.For<ITableMap>();
			map.Table.Returns(table);
			map.IdColumn.Returns(idColumn);

			var mapper = Substitute.For<IMapper>();
			mapper.GetTableMapFor<TestEntity>().Returns(Return(map));

			var options = create(mapper);

			return (opt?.Invoke(options) ?? options, new(table, map, idColumn, parts));
		}

		public sealed record Vars(
			ITable Table,
			ITableMap Map,
			IColumn IdColumn,
			QueryParts Parts
		);

		public record TestId(long Value) : StrongId(Value);

		public record TestEntity(TestId Id, int Foo, bool Bar) : IWithId<TestId>;

		public record TestOptions(IMapper Mapper) : QueryOptions<TestEntity, TestId>(Mapper);

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
}
