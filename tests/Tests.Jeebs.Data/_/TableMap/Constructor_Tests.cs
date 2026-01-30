// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;

namespace Jeebs.Data.TableMap_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Properties_Set()
	{
		// Arrange
		var name = new DbName(Rnd.Str);
		var columns = Substitute.For<IColumnList>();

		var table = Substitute.For<ITable>();
		table.GetName().Returns(name);
		var prop = Substitute.For<PropertyInfo>();
		prop.Name.Returns(Rnd.Str);
		var idColumn = Substitute.For<IColumn>();
		idColumn.PropertyInfo.Returns(prop);

		// Act
		var map = new TableMap(table, columns, idColumn);

		// Assert
		Assert.Equal(name, map.Name);
		Assert.Equal(columns, map.Columns);
		Assert.Equal(idColumn, idColumn);
	}
}
