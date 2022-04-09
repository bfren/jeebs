// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;

namespace Jeebs.Data.Map.MappedColumn_Tests;

public class ToString_Tests
{
	[Fact]
	public void Returns_Name()
	{
		// Arrange
		var name = Rnd.Str;
		var prop = Substitute.For<PropertyInfo>();
		prop.Name.Returns(Rnd.Str);
		var column = new MappedColumn(new DbName(Rnd.Str), name, prop);

		// Act
		var result = column.ToString();

		// Assert
		Assert.Equal(name, result);
	}
}
