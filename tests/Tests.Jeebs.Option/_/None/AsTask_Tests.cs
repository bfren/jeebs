// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.None_Tests
{
	public class AsTask_Tests
	{
		[Fact]
		public void Returns_None_As_Generic_Option()
		{
			// Arrange
			var none = Create.None<int>();

			// Act
			var result = none.AsTask;

			// Assert
			Assert.IsType<Task<Option<int>>>(result);
		}
	}
}
