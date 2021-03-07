// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Option_Tests
{
	public partial class Operator_Tests
	{
		[Fact]
		public void DoesNotEqual_When_Equal_Returns_False()
		{
			// Arrange
			var value = F.Rnd.Int;
			var some = Option.Wrap(value);

			// Act
			var result = some != value;

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void DoesNotEqual_When_Not_Equal_Returns_True()
		{
			// Arrange
			const int v0 = 18;
			const int v1 = 7;
			var some = Option.Wrap(v0);

			// Act
			var result = some != v1;

			// Assert
			Assert.True(result);
		}
	}
}
