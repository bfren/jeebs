// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Collections.ImmutableList_Tests;

public class Filter_Tests
{
	[Fact]
	public void Returns_Items_Matching_Predicate()
	{
		// Arrange
		var prefix = Rnd.Str;
		var i0 = prefix + Rnd.Str;
		var i1 = Rnd.Str;
		var i2 = prefix + Rnd.Str;
		var i3 = Rnd.Str;
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
