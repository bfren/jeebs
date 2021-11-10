// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Data.Mapping.TableName_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_Name()
	{
		// Arrange
		var name = F.Rnd.Str;

		// Act
		var result = new TableName(name);

		// Assert
		Assert.Null(result.Schema);
		Assert.Equal(name, result.Name);
	}

	[Fact]
	public void Sets_Schema_And_Name()
	{
		// Arrange
		var schema = F.Rnd.Str;
		var name = F.Rnd.Str;

		// Act
		var result = new TableName(schema, name);

		// Assert
		Assert.Equal(schema, result.Schema);
		Assert.Equal(name, result.Name);
	}
}
