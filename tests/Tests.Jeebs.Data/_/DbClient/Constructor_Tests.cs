// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.DbClient_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_Properties()
	{
		// Arrange
		var entities = Substitute.For<IEntityMapper>();
		var types = Substitute.For<IDbTypeMapper>();

		// Act
		var result = Substitute.ForPartsOf<DbClient>(entities, types);

		// Assert
		Assert.Same(entities, result.Entities);
		Assert.Same(types, result.Types);
	}
}
