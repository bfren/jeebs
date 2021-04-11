// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using Xunit;

namespace Jeebs.ImmutableList_Tests
{
	public class GetEnumerator_Tests
	{
		[Fact]
		public void Returns_Enumerator()
		{
			// Arrange
			var i0 = F.Rnd.Guid;
			var i1 = F.Rnd.Guid;
			var list = new ImmutableList<Guid>(new[] { i0, i1 });

			// Act
			var result = list.GetEnumerator();

			// Assert
			Assert.IsAssignableFrom<IEnumerator<Guid>>(result);
		}
	}
}
