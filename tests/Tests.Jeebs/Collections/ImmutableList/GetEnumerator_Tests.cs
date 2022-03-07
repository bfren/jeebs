// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Xunit;

namespace Jeebs.ImmutableList_Tests;

public class GetEnumerator_Tests
{
	[Fact]
	public void Returns_Enumerator()
	{
		// Arrange
		var i0 = F.Rnd.Guid;
		var i1 = F.Rnd.Guid;
		var list = ImmutableList.Create(new[] { i0, i1 });

		// Act
		var result = list.GetEnumerator();

		// Assert
		_ = Assert.IsAssignableFrom<IEnumerator<Guid>>(result);
	}
}
