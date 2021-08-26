// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.ImmutableList_Tests
{
	public class WithoutRange_Tests
	{
		[Fact]
		public void Returns_List_With_Items_Removed()
		{
			// Arrange
			var i0 = F.Rnd.Str;
			var i1 = F.Rnd.Str;
			var i2 = F.Rnd.Str;
			var i3 = F.Rnd.Str;
			var list = new ImmutableList<string>(new[] { i0, i1, i2, i3 });

			// Act
			var result = list.WithoutRange(new[] { i2, i3 });

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(i0, x),
				x => Assert.Equal(i1, x)
			);
		}

		[Fact]
		public void Returns_New_List_With_Items_Removed()
		{
			// Arrange
			var i0 = F.Rnd.Str;
			var i1 = F.Rnd.Str;
			var i2 = F.Rnd.Str;
			var i3 = F.Rnd.Str;
			var list = new ImmutableList<string>(new[] { i0, i1, i2, i3 });

			// Act
			var result = list.WithoutRange(new[] { i2, i3 });
			i0 = F.Rnd.Str;
			i1 = F.Rnd.Str;

			// Assert
			Assert.Collection(result,
				x => Assert.NotEqual(i0, x),
				x => Assert.NotEqual(i1, x)
			);
		}
	}
}
