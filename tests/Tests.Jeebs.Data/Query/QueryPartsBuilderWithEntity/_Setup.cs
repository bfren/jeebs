// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.QueryBuilder.QueryPartsBuilderWithEntity_Tests;

public abstract class QueryPartsBuilderWithEntity_Tests
{
	public static (TestBuilder builder, Vars v) Setup()
	{
		var mapper = Substitute.For<IEntityMapper>();

		var map = Substitute.For<ITableMap>();
		mapper.GetTableMapFor<TestEntity>().Returns(R.Wrap(map));

		var builder = Substitute.ForPartsOf<TestBuilder>(mapper);

		return (builder, new(mapper, map));
	}

	public sealed record class Vars(
		IEntityMapper Mapper,
		ITableMap Map
	);
}

public sealed record class TestId : LongId<TestId>;

public record class TestEntity(int Foo, bool Bar) : WithId<TestId, long>;

public abstract class TestBuilder(IEntityMapper mapper) : QueryPartsBuilderWithEntity<TestEntity, TestId>(mapper);
