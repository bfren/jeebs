using System;
using System.Collections.Generic;
using System.Text;
using Jx.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.Extract_Tests
{
	public class From_Tests
	{
		[Fact]
		public void No_Tables_Returns_Empty_List()
		{
			// Arrange

			// Act
			var result = Extract<Foo>.From();

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void No_Matching_Columns_Throws_NoColumnsExtractedException()
		{
			// Arrange
			var table = new FooTable();

			// Act
			void action() => Extract<FooNone>.From(table);

			// Assert
			var ex = Assert.Throws<NoColumnsExtractedException>(action);
			Assert.Equal(string.Format(NoColumnsExtractedException.Format, typeof(FooNone), table), ex.Message);
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
			Assert.Equal(3, result.Count);
			Assert.Equal(t0.ToString(), result[0].Table);
			Assert.Equal(t0.Id, result[0].Column);
			Assert.Equal(t0.Bar0, result[1].Column);
			Assert.Equal(t1.Bar2, result[2].Column);
		}

		public class FooCombined
		{
			[Id]
			public long Id { get; set; }

			public string Bar0 { get; set; } = string.Empty;

			public string Bar2 { get; set; } = string.Empty;
		}

		public class FooNone
		{
			public int NotBar { get; set; }
		}
	}
}
