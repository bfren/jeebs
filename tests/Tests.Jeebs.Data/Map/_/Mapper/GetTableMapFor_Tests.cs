// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using static Jeebs.Data.Map.Mapper.M;

namespace Jeebs.Data.Map.Mapper_Tests;

public class GetTableMapFor_Tests
{
	[Fact]
	public void Unmapped_Entity_Returns_None_With_TryingToGetUnmappedEntityMsg()
	{
		// Arrange
		using var mapper = new Mapper();

		// Act
		var result = mapper.GetTableMapFor<Foo>();

		// Assert
		var none = result.AssertNone();
		Assert.IsType<TryingToGetUnmappedEntityMsg<Foo>>(none);
	}

	[Fact]
	public void Mapped_Entity_Returns_Some_With_TableMap()
	{
		// Arrange
		using var mapper = new Mapper();
		var map = mapper.Map<Foo>(new FooTable());

		// Act
		var result = mapper.GetTableMapFor<Foo>();

		// Assert
		var some = result.AssertSome();
		Assert.Same(map, some);
	}
}
