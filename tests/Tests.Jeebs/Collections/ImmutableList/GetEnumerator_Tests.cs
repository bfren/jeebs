// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Collections.ImmutableList_Tests;

public class GetEnumerator_Tests
{
	[Fact]
	public void Returns_Enumerator()
	{
		// Arrange
		var i0 = Rnd.Guid;
		var i1 = Rnd.Guid;
		var list = ImmutableList.Create([i0, i1]);

		// Act
		var result = list.GetEnumerator();

		// Assert
		Assert.IsAssignableFrom<IEnumerator<Guid>>(result);
	}
}
