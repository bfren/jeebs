// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;

namespace Jeebs.Data.Extract_Tests;

public class From_Tests
{
	[Fact]
	public void No_Tables_Returns_Ok_With_Empty_List()
	{
		// Arrange

		// Act
		var result = Extract<Foo>.From();

		// Assert
		var ok = result.AssertOk();
		Assert.Empty(ok);
	}

	[Fact]
	public void No_Matching_Columns_Returns_Fail_With_NoColumnsExtractedFromTableMsg()
	{
		// Arrange
		var table = new FooTable();

		// Act
		var result = Extract<FooNone>.From(table);

		// Assert
		_ = result.AssertFailure("No columns were extracted from the tables.");
	}

	[Fact]
	public void Returns_Extracted_Columns_Without_Duplicates()
	{
		// Arrange
		var t0 = new FooTable();
		var t1 = new FooUnwriteableTable();
		var t2 = new FooDuplicateTable();

		// Act
		var result = Extract<FooCombined>.From(t0, t1, t2);

		// Assert
		var ok = result.AssertOk();
		Assert.Collection(ok,
			x => Assert.Equal((t0.GetName(), t0.FooId), (x.TblName, x.ColName)),
			x => Assert.Equal(t0.Bar0, x.ColName),
			x => Assert.Equal(t1.Bar2, x.ColName)
		);
	}

	public class FooCombined
	{
		[Id]
		public long FooId { get; set; }

		public string Bar0 { get; set; } = string.Empty;

		public string Bar2 { get; set; } = string.Empty;
	}

	public record class FooDuplicateTable : FooTable;

	public class FooNone
	{
		public int NotBar { get; set; }
	}
}
