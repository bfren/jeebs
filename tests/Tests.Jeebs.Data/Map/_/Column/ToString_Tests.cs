// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Map.Column_Tests;

public class ToString_Tests
{
	[Fact]
	public void Returns_Name()
	{
		// Arrange
		var name = Rnd.Str;
		var column = new Column(new DbName(Rnd.Str), name, Helpers.CreateInfoFromAlias());

		// Act
		var result = column.ToString();

		// Assert
		Assert.Equal(name, result);
	}
}
