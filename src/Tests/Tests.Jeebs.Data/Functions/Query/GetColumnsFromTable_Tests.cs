// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Mapping;
using Xunit;
using static F.DataF.QueryF;

namespace F.DataF.QueryF_Tests
{
	public class GetColumnsFromTable_Tests
	{
		[Fact]
		public void No_Matching_Columns_Returns_Empty_List()
		{
			// Arrange
			var table = new FooTable();

			// Act
			var result = GetColumnsFromTable<FooNone>(table);

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void Returns_Extracted_Columns()
		{
			// Arrange
			var table = new FooTable();

			// Act
			var result = GetColumnsFromTable<Foo>(table);

			// Assert
			Assert.Collection(result,
				x => Assert.Equal((table.GetName(), table.FooId), (x.Table, x.Name)),
				x => Assert.Equal(table.Bar0, x.Name),
				x => Assert.Equal(table.Bar1, x.Name)
			);
		}

		public class FooNone
		{
			public int NotBar { get; set; }
		}
	}
}
