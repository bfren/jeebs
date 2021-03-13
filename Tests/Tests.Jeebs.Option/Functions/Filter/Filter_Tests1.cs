// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public partial class Filter_Tests
	{
		[Fact]
		public void Returns_Only_Some_From_List()
		{
			// Arrange
			var v0 = Rnd.Int;
			var v1 = Rnd.Int;
			var o0 = Return(v0);
			var o1 = Return(v1);
			var o2 = None<int>(true);
			var o3 = None<int>(true);
			var list = new[] { o0, o1, o2, o3 };
			static string map(int x) => x.ToString();

			// Act
			var result = Filter(list, map, null);

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(v0.ToString(), x),
				x => Assert.Equal(v1.ToString(), x)
			);
		}

		[Fact]
		public void Returns_Matching_Some_From_List()
		{
			// Arrange
			var v0 = Rnd.Int;
			var v1 = Rnd.Int;
			var o0 = Return(v0);
			var o1 = Return(v1);
			var o2 = None<int>(true);
			var o3 = None<int>(true);
			var list = new[] { o0, o1, o2, o3 };
			static string map(int x) => x.ToString();

			// Act
			var result = Filter(list, map, x => x == v1);

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(v1.ToString(), x)
			);
		}
	}
}
