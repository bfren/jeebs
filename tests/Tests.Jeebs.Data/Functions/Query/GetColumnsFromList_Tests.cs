// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data;
using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;
using static F.DataF.QueryF;

namespace F.DataF.QueryF_Tests
{
	public class GetColumnsFromList_Tests
	{
		[Fact]
		public void No_Columns_Returns_Empty_String()
		{
			// Arrange
			var client = Substitute.For<IDbClient>();
			var columns = new ColumnList();

			// Act
			var result = GetColumnsFromList(client, columns);

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
			var columns = new ColumnList(new[] { c0, c1 });

			// Act
			var result = GetColumnsFromList(client, columns);

			// Assert
			client.Received(1).Escape(c0, true);
			client.Received(1).Escape(c1, true);
			Assert.Equal(2, result.Count);
		}
	}
}
