// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using static Jeebs.Data.Map.EntityMapper.M;

namespace Jeebs.Data.Map.EntityMapper_Tests;

public class GetTableMapFor_Tests
{
	[Fact]
	public void Unmapped_Entity_Returns_None_With_TryingToGetUnmappedEntityMsg()
	{
		// Arrange
		using var mapper = new EntityMapper();

		// Act
		var result = mapper.GetTableMapFor<Foo>();

		// Assert
		result.AssertNone().AssertType<TryingToGetUnmappedEntityMsg<Foo>>();
	}

	[Fact]
	public void Mapped_Entity_Returns_Some_With_TableMap()
	{
		// Arrange
		using var mapper = new EntityMapper();
		var map = mapper.Map<Foo, FooTable>(new());

		// Act
		var result = mapper.GetTableMapFor<Foo>();

		// Assert
		var some = result.AssertSome();
		Assert.Same(map, some);
	}
}
