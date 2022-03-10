// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.DbClient_Tests;

public class GetSetListForUpdateQuery_Tests
{
	[Fact]
	public void No_Columns_Returns_Empty_List()
	{
		// Arrange
		var client = Substitute.ForPartsOf<DbClient>();
		var columns = new ColumnList();

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
		_ = client.Escape(Arg.Any<IColumn>()).Returns(x => $"--{x.ArgAt<IColumn>(0).ColName}--");
		_ = client.GetParamRef(Arg.Any<string>()).Returns(x => $"##{x.ArgAt<string>(0)}##");

		var name = Rnd.Str;
		var alias = Rnd.Str;
		var column = new Column(new TableName(Rnd.Str), name, alias);
		var expected = $"--{name}-- = ##{alias}##";

		var columns = new ColumnList(new[] { column });

		// Act
		var result = client.GetSetListForUpdateQueryTest(columns);

		// Assert
		_ = client.Received().Escape(column);
		_ = client.Received().GetParamRef(alias);
		Assert.Collection(result,
			x => Assert.Equal(expected, x)
		);
	}
}
