// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Map.TableName_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_Name()
	{
		// Arrange
		var name = Rnd.Str;

		// Act
		var result = new DbName(name);

		// Assert
		Assert.Null(result.Schema);
		Assert.Equal(name, result.Name);
	}

	[Fact]
	public void Sets_Schema_And_Name()
	{
		// Arrange
		var schema = Rnd.Str;
		var name = Rnd.Str;

		// Act
		var result = new DbName(schema, name);

		// Assert
		Assert.Equal(schema, result.Schema);
		Assert.Equal(name, result.Name);
	}
}
