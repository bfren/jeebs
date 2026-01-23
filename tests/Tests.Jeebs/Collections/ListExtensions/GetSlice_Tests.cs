// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Collections.ListExtensions_Tests;

public class GetSlice_Tests
{
	[Fact]
	public void Returns_Correct_Slice()
	{
		// Arrange
		var v0 = Rnd.Str;
		var v1 = Rnd.Str;
		var v2 = Rnd.Str;
		var v3 = Rnd.Str;
		var v4 = Rnd.Str;
		var v5 = Rnd.Str;
		var v6 = Rnd.Str;
		var v7 = Rnd.Str;
		var v8 = Rnd.Str;
		var v9 = Rnd.Str;
		var list = new List<string> { v0, v1, v2, v3, v4, v5, v6, v7, v8, v9 };
		var expected = new List<string> { v3, v4 };

		// Act
		var result = list.GetSlice(3..5);

		// Assert
		Assert.Equal(expected, result);
	}
}
