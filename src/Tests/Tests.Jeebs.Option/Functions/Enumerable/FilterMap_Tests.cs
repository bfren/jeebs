// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Enumerable;

namespace F.OptionFEnumerable_Tests
{
	public class FilterMap_Tests
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
			var result = FilterMap(list, map, null);

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
			var predicate = Substitute.For<Func<int, bool>>();
			predicate.Invoke(v1).Returns(true);

			// Act
			var result = FilterMap(list, map, predicate);

			// Assert
			Assert.Collection(result,
				x => Assert.Equal(v1.ToString(), x)
			);
		}
	}
}
