// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Mapping;
using NSubstitute;

namespace Jeebs.Data.Querying.QueryPartsBuilderWithEntity_Tests
{
	public abstract class QueryPartsBuilderWithEntity_Tests
	{
		public static (TestBuilder builder, Vars v) Setup()
		{
			var mapper = Substitute.For<IMapper>();

			var map = Substitute.For<ITableMap>();
			mapper.GetTableMapFor<TestEntity>().Returns(map.Return());

			var builder = Substitute.ForPartsOf<TestBuilder>(mapper);

			return (builder, new(mapper, map));
		}

		public sealed record Vars(
			IMapper Mapper,
			ITableMap Map
		);
	}

	public record TestId(ulong Value) : StrongId(Value)
	{
		public TestId() : this(0) { }
	}

	public record TestEntity(TestId Id, int Foo, bool Bar) : IWithId<TestId>;

	public abstract class TestBuilder : QueryPartsBuilderWithEntity<TestEntity, TestId>
	{
		protected TestBuilder(IMapper mapper) : base(mapper) { }
	}
}
