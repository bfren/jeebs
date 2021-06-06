// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Collections.Generic;
using Jeebs.Data;
using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;
using static F.DataF.QueryF;

namespace F.DataF.QueryF_Tests
{
	public class GetSelectFromList_Tests
	{
		[Fact]
		public void No_Columns_Returns_Empty_String()
		{
			// Arrange
			var client = Substitute.For<IDbClient>();
			var columns = new ColumnList();

			// Act
			var result = GetSelectFromList(client, columns);

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
			_ = GetSelectFromList(client, columns);

			// Assert
			client.Received(1).EscapeWithTable(c0, true);
			client.Received(1).EscapeWithTable(c1, true);
			client.ReceivedWithAnyArgs(1).JoinList(Arg.Any<List<string>>(), false);
		}
	}
}
