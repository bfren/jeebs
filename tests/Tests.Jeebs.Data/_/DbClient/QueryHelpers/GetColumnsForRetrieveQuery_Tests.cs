// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.DbClient_Tests;

public class GetColumnsForRetrieveQuery_Tests
{
	[Fact]
	public void No_Columns_Returns_Empty_List()
	{
		// Arrange
		var client = Substitute.ForPartsOf<DbClient>();
		var list = new ColumnList();

		// Act
		var col = client.GetColumnsForRetrieveQueryTest(list);

		// Assert
		Assert.Empty(col);
	}

	[Fact]
	public void Returns_Escaped_Column_Names()
	{
		// Arrange
		var client = Substitute.ForPartsOf<DbClient>();
		var column = new Column(new TableName(Rnd.Str), Rnd.Str, Rnd.Str);
		var list = new ColumnList(new[] { column });

		// Act
		client.GetColumnsForRetrieveQueryTest(list);

		// Assert
		client.Received().Escape(column, true);
	}
}
