// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Map.TableMap_Tests;

public class ToString_Tests
{
	[Fact]
	public void Returns_Name()
	{
		// Arrange
		var name = new TableName(Rnd.Str);
		var table = Substitute.For<ITable>();
		_ = table.GetName().Returns(name);
		var map = new TableMap(table, Substitute.For<IMappedColumnList>(), GetColumnNames_Tests.Get().column);

		// Act
		var result = map.ToString();

		// Assert
		Assert.Equal(name.GetFullName(s => s), result);
	}
}
