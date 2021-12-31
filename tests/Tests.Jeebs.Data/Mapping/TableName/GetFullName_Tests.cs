// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Data.Mapping.TableName_Tests;

public class GetFullName_Tests
{
	[Fact]
	public void Without_Schema_Returns_Escaped_Name()
	{
		// Arrange
		var name = F.Rnd.Str;
		var table = new TableName(name);
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
		var schema = F.Rnd.Str;
		var name = F.Rnd.Str;
		var table = new TableName(schema, name);
		var escape = string (string obj) => $"<{obj}>";

		// Act
		var result = table.GetFullName(escape);

		// Assert
		Assert.Equal($"{escape(schema)}{TableName.SchemaSeparator}{escape(name)}", result);
	}
}
