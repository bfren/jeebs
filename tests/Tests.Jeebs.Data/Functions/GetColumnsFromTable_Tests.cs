// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.DataF_Tests;

public class GetColumnsFromTable_Tests
{
	[Fact]
	public void No_Matching_Columns_Returns_Empty_List()
	{
		// Arrange
		var table = new FooTable();

		// Act
		var result = DataF.GetColumnsFromTable<FooNone>(table);

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public void Returns_Extracted_Columns()
	{
		// Arrange
		var table = new FooTable();

		// Act
		var result = DataF.GetColumnsFromTable<Foo>(table);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal((table.GetName(), table.FooId), (x.TblName, x.ColName)),
			x => Assert.Equal(table.Bar0, x.ColName),
			x => Assert.Equal(table.Bar1, x.ColName)
		);
	}

	public class FooNone
	{
		public int NotBar { get; set; }
	}
}
