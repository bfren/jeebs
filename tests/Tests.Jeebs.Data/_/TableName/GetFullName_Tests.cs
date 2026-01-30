// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.TableName_Tests;

public class GetFullName_Tests
{
	[Fact]
	public void Without_Schema_Returns_Escaped_Name()
	{
		// Arrange
		var name = Rnd.Str;
		var table = new DbName(name);
		var escape = string (string obj) => $"<{obj}>";

		// Act
		var result = table.GetFullName(escape);

		// Assert
		Assert.Equal(escape(name), result);
	}

	[Fact]
	public void With_Schema_Returns_Escaped_Schema_And_Name()
	{
		// Arrange
		var schema = Rnd.Str;
		var name = Rnd.Str;
		var table = new DbName(schema, name);
		var escape = string (string obj) => $"<{obj}>";

		// Act
		var result = table.GetFullName(escape);

		// Assert
		Assert.Equal($"{escape(schema)}{DbName.SchemaSeparator}{escape(name)}", result);
	}
}
