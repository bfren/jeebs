// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Data.Mapping.Column_Tests;

public class ToString_Tests
{
	[Fact]
	public void Returns_Name()
	{
		// Arrange
		var name = F.Rnd.Str;
		var column = new Column(new TableName(F.Rnd.Str), name, F.Rnd.Str);

		// Act
		var result = column.ToString();

		// Assert
		Assert.Equal(name, result);
	}
}
