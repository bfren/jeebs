// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;
using Jeebs.Data.Map;

namespace Jeebs.Data.DbClient_Tests;

public class GetColumnsForCreateQuery_Tests
{
	[Fact]
	public void No_Mapped_Columns_Returns_Empty_Lists()
	{
		// Arrange
		var client = Substitute.ForPartsOf<DbClient>();
		var list = new ColumnList();

		// Act
		var (col, par) = client.GetColumnsForCreateQueryTest(list);

		// Assert
		Assert.Empty(col);
		Assert.Empty(par);
	}

	[Fact]
	public void Returns_Escaped_Column_Names_And_Parameter_Refs()
	{
		// Arrange
		var client = Substitute.ForPartsOf<DbClient>();

		var name = Rnd.Str;

		var alias = Rnd.Str;
		var propertyInfo = Substitute.For<PropertyInfo>();
		propertyInfo.Name.Returns(alias);

		var column = new Column(new DbName(Rnd.Str), name, propertyInfo);

		var list = new ColumnList(new[] { column });

		// Act
		client.GetColumnsForCreateQueryTest(list);

		// Assert
		client.Received().Escape(column);
		client.Received().GetParamRef(alias);
	}
}
