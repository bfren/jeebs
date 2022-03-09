// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Collections.ImmutableList_Tests;

public class GetHashCode_Tests
{
	[Fact]
	public void Returns_Internal_List_HashCode()
	{
		// Arrange
		var items = new[] { Rnd.Str, Rnd.Str, Rnd.Str };
		var sys = System.Collections.Immutable.ImmutableList<string>.Empty.AddRange(items);
		var list = new ImmutableList<string> { List = sys };

		// Act
		var result = list.GetHashCode();

		// Assert
		Assert.Equal(sys.GetHashCode(), result);
	}
}
