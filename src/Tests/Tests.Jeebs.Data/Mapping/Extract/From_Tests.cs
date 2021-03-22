﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static Jeebs.Data.ExtractMsg;

namespace Jeebs.Data.Mapping.Extract_Tests
{
	public class From_Tests
	{
		[Fact]
		public void No_Tables_Returns_Some_With_Empty_List()
		{
			// Arrange

			// Act
			var result = Extract<Foo>.From();

			// Assert
			var some = result.AssertSome();
			Assert.Empty(some);
		}

		[Fact]
		public void No_Matching_Columns_Returns_None_With_NoColumnsExtractedFromTableMsg()
		{
			// Arrange
			var table = new FooTable();

			// Act
			var result = Extract<FooNone>.From(table);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<NoColumnsExtractedFromTableMsg>(none);
		}

		[Fact]
		public void Returns_Extracted_Columns()
		{
			// Arrange
			var t0 = new FooTable();
			var t1 = new FooUnwriteableTable();

			// Act
			var result = Extract<FooCombined>.From(t0, t1);

			// Assert
			var some = result.AssertSome();
			Assert.Collection(some,
				x => Assert.Equal((t0.ToString(), t0.FooId), (x.Table, x.Name)),
				x => Assert.Equal(t0.Bar0, x.Name),
				x => Assert.Equal(t1.Bar2, x.Name)
			);
		}

		public class FooCombined
		{
			[Id]
			public long FooId { get; set; }

			public string Bar0 { get; set; } = string.Empty;

			public string Bar2 { get; set; } = string.Empty;
		}

		public class FooNone
		{
			public int NotBar { get; set; }
		}
	}
}
