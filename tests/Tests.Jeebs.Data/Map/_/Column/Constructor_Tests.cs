// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Map.Column_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Properties_Set()
	{
		// Arrange
		var table = new DbName(Rnd.Str);
		var name = Rnd.Str;
		var alias = Rnd.Str;
		var column = new Column(table, name, alias);

		// Act

		// Assert
		Assert.Equal(table, column.TblName);
		Assert.Equal(name, column.ColName);
		Assert.Equal(alias, column.ColAlias);
	}
}
