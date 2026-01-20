// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

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
		_ = result.AssertFail("");
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
		var ok = result.AssertOk();
		Assert.Same(map, ok);
	}
}
