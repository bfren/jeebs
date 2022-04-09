// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;
using Jeebs.Data.Map;

namespace Jeebs.Data.DbClient_Tests;

public class GetSetListForUpdateQuery_Tests
{
	[Fact]
	public void No_Columns_Returns_Empty_List()
	{
		// Arrange
		var client = Substitute.ForPartsOf<DbClient>();
		var columns = new MappedColumnList();

		// Act
		var result = client.GetSetListForUpdateQueryTest(columns);

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public void Returns_Escaped_Column_Names_And_Aliases()
	{
		// Arrange
		var client = Substitute.ForPartsOf<DbClient>();
		client.Escape(Arg.Any<IColumn>()).Returns(x => $"--{x.ArgAt<IColumn>(0).ColName}--");
		client.GetParamRef(Arg.Any<string>()).Returns(x => $"##{x.ArgAt<string>(0)}##");

		var name = Rnd.Str;
		var alias = Rnd.Str;
		var propertyInfo = Substitute.For<PropertyInfo>();
		propertyInfo.Name.Returns(alias);
		var column = new MappedColumn(new DbName(Rnd.Str), name, propertyInfo);
		var expected = $"--{name}-- = ##{alias}##";

		var columns = new MappedColumnList(new[] { column });

		// Act
		var result = client.GetSetListForUpdateQueryTest(columns);

		// Assert
		client.Received().Escape(column);
		client.Received().GetParamRef(alias);
		Assert.Collection(result,
			x => Assert.Equal(expected, x)
		);
	}
}
