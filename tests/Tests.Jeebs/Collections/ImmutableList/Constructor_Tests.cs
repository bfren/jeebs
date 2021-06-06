// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.ImmutableList_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Parameterless_Creates_Empty_List()
		{
			// Arrange

			// Act
			var result = new ImmutableList<string>();

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void With_Collection_Creates_From_Collection()
		{
			// Arrange
			var i0 = F.Rnd.Str;
			var i1 = F.Rnd.Str;

			// Act
			var result = new ImmutableList<string>(new[] { i0, i1 });

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(i0, x),
				x => Assert.Equal(i1, x)
			);
		}
	}
}
