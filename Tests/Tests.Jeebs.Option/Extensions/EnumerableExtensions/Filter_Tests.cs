// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static F.OptionF;

namespace Jeebs.EnumerableExtensions_Tests
{
	public class Filter_Tests
	{
		[Fact]
		public void Returns_Only_Some()
		{
			// Arrange
			var v0 = F.Rnd.Int;
			var v1 = F.Rnd.Int;
			var o0 = Return(v0);
			var o1 = Return(v1);
			var o2 = None<int>(true);
			var o3 = None<int>(true);
			var list = new[] { o0, o1, o2, o3 };

			// Act
			var result = list.Filter();

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(v0, x),
				x => Assert.Equal(v1, x)
			);
		}

		[Fact]
		public void Returns_Matching_Some()
		{
			// Arrange
			var v0 = F.Rnd.Int;
			var v1 = F.Rnd.Int;
			var o0 = Return(v0);
			var o1 = Return(v1);
			var o2 = None<int>(true);
			var o3 = None<int>(true);
			var list = new[] { o0, o1, o2, o3 };

			// Act
			var result = list.Filter(x => x == v1);

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(v1, x)
			);
		}
	}
}
