// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using MaybeF.Extensions;
using StrongId;

namespace Jeebs.Data.Query.QueryPartsBuilderWithEntity_Tests;

public abstract class QueryPartsBuilderWithEntity_Tests
{
	public static (TestBuilder builder, Vars v) Setup()
	{
		var mapper = Substitute.For<IEntityMapper>();

		var map = Substitute.For<ITableMap>();
		mapper.GetTableMapFor<TestEntity>().Returns(map.Some());

		var builder = Substitute.ForPartsOf<TestBuilder>(mapper);

		return (builder, new(mapper, map));
	}

	public sealed record class Vars(
		IEntityMapper Mapper,
		ITableMap Map
	);
}

public sealed record class TestId : LongId;

public record class TestEntity(TestId Id, int Foo, bool Bar) : IWithId<TestId>;

public abstract class TestBuilder : QueryPartsBuilderWithEntity<TestEntity, TestId>
{
	protected TestBuilder(IEntityMapper mapper) : base(mapper) { }
}
