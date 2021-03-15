// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Linq;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs.EnumerableExtensions_Tests
{
	public class FilterMap_Tests
	{
		[Fact]
		public void Returns_Only_Some_From_List()
		{
			// Arrange
			var v0 = F.Rnd.Int;
			var v1 = F.Rnd.Int;
			var o0 = Return(v0);
			var o1 = Return(v1);
			var o2 = None<int>(true);
			var o3 = None<int>(true);
			var list = new[] { o0, o1, o2, o3 };

			static string m0(int x) => x.ToString();
			static Option<string> m1(int x) => x.ToString();

			// Act
			var r0 = list.FilterMap(m0);
			var r1 = list.FilterMap(m1);

			// Assert
			Assert.Collection(r0,
				x => Assert.Equal(v0.ToString(), x),
				x => Assert.Equal(v1.ToString(), x)
			);
			Assert.Collection(r1,
				x =>
				{
					var s0 = x.AssertSome();
					Assert.Equal(v0.ToString(), s0);
				},
				x =>
				{
					var s1 = x.AssertSome();
					Assert.Equal(v1.ToString(), s1);
				}
			);
		}

		[Fact]
		public void Returns_Matching_Some_From_List()
		{
			// Arrange
			var v0 = F.Rnd.Int;
			var v1 = F.Rnd.Int;
			var o0 = Return(v0);
			var o1 = Return(v1);
			var o2 = None<int>(true);
			var o3 = None<int>(true);
			var list = new[] { o0, o1, o2, o3 };

			static string m0(int x) => x.ToString();
			static Option<string> m1(int x) => x.ToString();

			var predicate = Substitute.For<Func<int, bool>>();
			predicate.Invoke(v1).Returns(true);

			// Act
			var r0 = list.FilterMap(m0, predicate);
			var r1 = list.FilterMap(m1, predicate);

			// Assert
			Assert.Collection(r0,
				x => Assert.Equal(v1.ToString(), x)
			);
			Assert.Collection(r1,
				x =>
				{
					var some = x.AssertSome();
					Assert.Equal(v1.ToString(), some);
				}
			);
		}
	}
}
