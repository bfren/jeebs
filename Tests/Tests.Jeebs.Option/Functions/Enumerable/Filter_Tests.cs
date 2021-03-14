// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Enumerable;

namespace F.OptionFEnumerable_Tests
{
	public class Filter_Tests
	{
		[Fact]
		public void Maps_And_Returns_Only_Some_From_List()
		{
			// Arrange
			var v0 = Rnd.Int;
			var v1 = Rnd.Int;
			var o0 = Return(v0);
			var o1 = Return(v1);
			var o2 = None<int>(true);
			var o3 = None<int>(true);
			var list = new[] { o0, o1, o2, o3 };

			// Act
			var result = Filter(list, null);

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(v0, x),
				x => Assert.Equal(v1, x)
			);
		}

		[Fact]
		public void Maps_And_Returns_Matching_Some_From_List()
		{
			// Arrange
			var v0 = Rnd.Int;
			var v1 = Rnd.Int;
			var o0 = Return(v0);
			var o1 = Return(v1);
			var o2 = None<int>(true);
			var o3 = None<int>(true);
			var list = new[] { o0, o1, o2, o3 };
			var predicate = Substitute.For<Func<int, bool>>();
			predicate.Invoke(v1).Returns(true);

			// Act
			var result = Filter(list, predicate);

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(v1, x)
			);
		}
	}
}
