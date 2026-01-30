// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Column_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Properties_Set()
	{
		// Arrange
		var table = new DbName(Rnd.Str);
		var name = Rnd.Str;
		var alias = Rnd.Str;
		var prop = Helpers.CreateInfoFromAlias(alias);

		// Act
		var result = new Column(table, name, prop);

		// Assert
		Assert.Equal(table, result.TblName);
		Assert.Equal(name, result.ColName);
		Assert.Equal(alias, result.ColAlias);
		Assert.Equal(prop, result.PropertyInfo);
	}
}
