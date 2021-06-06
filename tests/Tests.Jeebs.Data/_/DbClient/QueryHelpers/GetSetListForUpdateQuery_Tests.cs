// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.DbClient_Tests
{
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
			client.Escape(Arg.Any<IColumn>()).Returns(x => $"--{x.ArgAt<IColumn>(0).Name}--");
			client.GetParamRef(Arg.Any<string>()).Returns(x => $"##{x.ArgAt<string>(0)}##");

			var name = F.Rnd.Str;
			var alias = F.Rnd.Str;
			var column = new Column(F.Rnd.Str, name, alias);
			var expected = $"--{name}-- = ##{alias}##";

			var columns = new ColumnList(new[] { column });

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
}
