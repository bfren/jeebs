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

			var map = Substitute.For<ITableMap>();
			map.Table.Returns(table);

			var parts = new QueryParts(table);

			var mapper = Substitute.For<IMapper>();
			mapper.GetTableMapFor<TestEntity>().Returns(Return(map));

			var options = new TestOptions(mapper);

			return new(table, map, parts, opt?.Invoke(options) ?? options);
		}

		public sealed record Vars(
			ITable Table,
			ITableMap Map,
			QueryParts Parts,
			TestOptions Options
		);

		public sealed record TestId(long Value) : StrongId(Value);

		public sealed record TestEntity(TestId Id, int Foo, bool Bar) : IWithId<TestId>;

		public sealed record TestOptions(IMapper Mapper) : QueryOptions<TestEntity, TestId>(Mapper);
	}
}
