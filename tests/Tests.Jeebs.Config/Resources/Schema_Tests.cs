// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.Resources;

public class Schema_Tests
{
	[Fact]
	public void Returns_Byte_Array()
	{
		// Arrange

		// Act
		var result = Properties.Resources.schema;

		// Assert
		Assert.IsType<byte[]>(result);
	}
}
