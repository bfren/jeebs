// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.Common.DbClient_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_Properties()
	{
		// Arrange
		var adapter = Substitute.For<IAdapter>();
		var mapper = Substitute.For<IEntityMapper>();

		// Act
		var result = Substitute.ForPartsOf<DbClient>(adapter, mapper);

		// Assert
		Assert.Same(adapter, result.Adapter);
		Assert.Same(mapper, result.EntityMapper);
	}
}
