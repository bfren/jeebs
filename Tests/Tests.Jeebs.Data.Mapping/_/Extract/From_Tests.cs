using Jx.Data.Mapping;
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
			Assert.Collection(result,
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
