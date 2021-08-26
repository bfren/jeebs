// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.ImmutableList_Tests
{
	public class Filter_Tests
	{
		[Fact]
		public void Returns_Items_Matching_Predicate()
		{
			// Arrange
			var prefix = F.Rnd.Str;
			var i0 = prefix + F.Rnd.Str;
			var i1 = F.Rnd.Str;
			var i2 = prefix + F.Rnd.Str;
			var i3 = F.Rnd.Str;
			var list = ImmutableList.Create(i0, i1, i2, i3);

			// Act
			var result = list.Where(s => s.StartsWith(prefix));

			// Assert
			Assert.Collection(result,
				x => Assert.Same(x, i0),
				x => Assert.Same(x, i2)
			);
		}
	}
}
