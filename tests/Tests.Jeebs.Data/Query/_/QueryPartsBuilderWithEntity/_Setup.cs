// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using Jeebs.Id;
using MaybeF.Extensions;

namespace Jeebs.Data.Query.QueryPartsBuilderWithEntity_Tests;

public abstract class QueryPartsBuilderWithEntity_Tests
{
	public static (TestBuilder builder, Vars v) Setup()
	{
		var mapper = Substitute.For<IMapper>();

		var map = Substitute.For<ITableMap>();
		mapper.GetTableMapFor<TestEntity>().Returns(map.Some());

		var builder = Substitute.ForPartsOf<TestBuilder>(mapper);

		return (builder, new(mapper, map));
	}

	public sealed record class Vars(
		IMapper Mapper,
		ITableMap Map
	);
}

public readonly record struct TestId(long Value) : IStrongId;

public record class TestEntity(TestId Id, int Foo, bool Bar) : IWithId<TestId>;

public abstract class TestBuilder : QueryPartsBuilderWithEntity<TestEntity, TestId>
{
	protected TestBuilder(IMapper mapper) : base(mapper) { }
}
