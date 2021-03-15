// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.ObjectExtensions_Tests
{
	public class ReturnIf_Tests
	{
		[Fact]
		public void True_Returns_Some()
		{
			// Arrange
			var value = F.Rnd.Int;

			// Act
			var r0 = value.ReturnIf(() => true);

			// Assert
			var s0 = r0.AssertSome();
			Assert.Equal(value, s0);
		}

		[Fact]
		public void False_Returns_None()
		{
			// Arrange
			var value = F.Rnd.Int;

			// Act
			var result = value.ReturnIf(() => false);

			// Assert
			result.AssertNone();
		}
	}
}
