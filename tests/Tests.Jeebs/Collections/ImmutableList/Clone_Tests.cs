// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.ImmutableList_Tests
{
	public class Clone_Tests
	{
		[Fact]
		public void Returns_Copy()
		{
			// Arrange
			var i0 = new Test();
			var i1 = new Test();
			var list = new ImmutableList<Test>(new[] { i0, i1 });

			// Act
			var result = list.Clone();
			i0 = new();
			i1 = new();

			// Assert
			Assert.Collection(result,
				x => Assert.NotSame(i0, x),
				x => Assert.NotSame(i1, x)
			);
		}

		public sealed class Test { }
	}
}
