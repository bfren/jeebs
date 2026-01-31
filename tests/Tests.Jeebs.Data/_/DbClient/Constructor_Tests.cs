// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.DbClient_Tests;

public class Constructor_Tests : DbClient_Setup
{
	[Fact]
	public void Sets_Properties()
	{
		// Arrange
		var (_, v) = Setup();

		// Act
		var result = Substitute.ForPartsOf<DbClient>(v.Entities);

		// Assert
		Assert.Same(v.Entities, result.Entities);
	}
}
