// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public class CreateParts_Tests
	{
		[Fact]
		public void Returns_New_QueryParts_With_Table()
		{
			// Arrange
			var (table, _, _, _, options) = QueryOptions_Setup.Get();
			var cols = Substitute.For<IColumnList>();

			// Act
			var result = options.CreatePartsTest(table, cols);

			// Assert
			var some = result.AssertSome();
			Assert.Same(table, some.From);
		}

		[Fact]
		public void Returns_New_QueryParts_With_Select()
		{
			// Arrange
			var (table, _, _, _, options) = QueryOptions_Setup.Get();
			var cols = Substitute.For<IColumnList>();

			// Act
			var result = options.CreatePartsTest(table, cols);

			// Assert
			var some = result.AssertSome();
			Assert.Same(cols, some.Select);
		}

		[Fact]
		public void Returns_New_QueryParts_With_Maximum()
		{
			// Arrange
			var maximum = F.Rnd.Lng;
			var (table, _, _, _, options) = QueryOptions_Setup.Get(opt => opt with { Maximum = maximum });
			var cols = Substitute.For<IColumnList>();

			// Act
			var result = options.CreatePartsTest(table, cols);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(maximum, some.Maximum);
		}

		[Fact]
		public void Returns_New_QueryParts_With_Skip()
		{
			// Arrange
			var skip = F.Rnd.Lng;
			var (table, _, _, _, options) = QueryOptions_Setup.Get(opt => opt with { Skip = skip });
			var cols = Substitute.For<IColumnList>();

			// Act
			var result = options.CreatePartsTest(table, cols);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(skip, some.Skip);
		}
	}
}
