// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.TableMap_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Properties_Set()
	{
		// Arrange
		var name = new TableName(F.Rnd.Str);
		var columns = Substitute.For<IMappedColumnList>();

		var table = Substitute.For<ITable>();
		_ = table.GetName().Returns(name);
		var prop = Substitute.For<PropertyInfo>();
		_ = prop.Name.Returns(F.Rnd.Str);
		var idColumn = Substitute.For<IMappedColumn>();
		_ = idColumn.PropertyInfo.Returns(prop);

		// Act
		var map = new TableMap(table, columns, idColumn);

		// Assert
		Assert.Equal(name, map.Name);
		Assert.Equal(columns, map.Columns);
		Assert.Equal(idColumn, idColumn);
	}
}
