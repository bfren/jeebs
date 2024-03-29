// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;

namespace Jeebs.Data.Map.TableMap_Tests;

public class GetColumnAliases_Tests
{
	internal static (string alias, Column column) Get()
	{
		var alias = Rnd.Str;
		var prop = Substitute.For<PropertyInfo>();
		prop.Name.Returns(alias);
		return (alias, new Column(new DbName(Rnd.Str), Rnd.Str, prop));
	}

	[Fact]
	public void No_Columns_Returns_Empty_List()
	{
		// Arrange
		var table = Substitute.For<ITable>();
		var map = new TableMap(table, new ColumnList(), Get().column);

		// Act
		var result = map.GetColumnAliases(false);

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public void Selects_Column_Aliases_With_Id_Alias()
	{
		// Arrange
		var (a0, c0) = Get();
		var (a1, c1) = Get();
		var (a2, c2) = Get();
		var (a3, c3) = Get();
		var table = Substitute.For<ITable>();
		var map = new TableMap(table, new ColumnList(new[] { c0, c1, c2, c3 }), c0);

		// Act
		var result = map.GetColumnAliases(true);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(a0, x),
			x => Assert.Equal(a1, x),
			x => Assert.Equal(a2, x),
			x => Assert.Equal(a3, x)
		);
	}

	[Fact]
	public void Selects_Column_Aliases_Without_Id_Alias()
	{
		// Arrange
		var (a0, c0) = Get();
		var (a1, c1) = Get();
		var (a2, c2) = Get();
		var (a3, c3) = Get();
		var table = Substitute.For<ITable>();
		var map = new TableMap(table, new ColumnList(new[] { c0, c1, c2, c3 }), c0);

		// Act
		var result = map.GetColumnAliases(false);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(a1, x),
			x => Assert.Equal(a2, x),
			x => Assert.Equal(a3, x)
		);
	}
}
