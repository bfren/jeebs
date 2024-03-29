﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Collections.ImmutableList_Tests;

public class Merge_Tests
{
	[Fact]
	public void Returns_Merged_Lists()
	{
		// Arrange
		var v0 = Rnd.Str;
		var v1 = Rnd.Str;
		var v2 = Rnd.Str;
		var v3 = Rnd.Str;
		var l0 = ImmutableList.Create(v0, v1);
		var l1 = ImmutableList.Create(v2, v3);
		var expected = ImmutableList.Create(v0, v1, v2, v3);

		// Act
		var result = ImmutableList.Merge(l0, l1);

		// Assert
		Assert.Equal(expected, result);
	}
}
