// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.Query.Functions.QueryF_Tests;

public class GetSelectFromList_Tests
{
	[Fact]
	public void No_Columns_Returns_Empty_String()
	{
		// Arrange
		var client = Substitute.For<IDbClient>();
		var columns = new ColumnList();

		// Act
		var result = QueryF.GetSelectFromList(client, columns);

		// Assert
		Assert.Equal(string.Empty, result);
		client.DidNotReceiveWithAnyArgs().EscapeWithTable(Arg.Any<IColumn>(), true);
		client.DidNotReceiveWithAnyArgs().JoinList(Arg.Any<List<string>>(), false);
	}

	[Fact]
	public void Escapes_And_Joins_Columns()
	{
		// Arrange
		var client = Substitute.For<IDbClient>();
		var c0 = Substitute.For<IColumn>();
		var c1 = Substitute.For<IColumn>();
		var columns = new ColumnList(new[] { c0, c1 });

		// Act
		QueryF.GetSelectFromList(client, columns);

		// Assert
		client.Received(1).EscapeWithTable(c0, true);
		client.Received(1).EscapeWithTable(c1, true);
		client.ReceivedWithAnyArgs(1).JoinList(Arg.Any<List<string>>(), false);
	}
}
