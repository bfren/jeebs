// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.Common.FluentQuery.FluentQueryF_Tests;

public class GetColumnsFromList_Tests
{
	[Fact]
	public void No_Columns_Returns_Empty_String()
	{
		// Arrange
		var client = Substitute.For<IDbClient>();
		var columns = new ColumnList();

		// Act
		var result = QueryF.GetColumnsFromList(client, columns);

		// Assert
		Assert.Empty(result);
		client.DidNotReceiveWithAnyArgs().Escape(Arg.Any<IColumn>(), true);
	}

	[Fact]
	public void Escapes_And_Joins_Columns()
	{
		// Arrange
		var client = Substitute.For<IDbClient>();
		var c0 = Substitute.For<IColumn>();
		var c1 = Substitute.For<IColumn>();
		var columns = new ColumnList([c0, c1]);

		// Act
		var result = QueryF.GetColumnsFromList(client, columns);

		// Assert
		client.Received(1).Escape(c0, true);
		client.Received(1).Escape(c1, true);
		Assert.Equal(2, result.Count);
	}
}
