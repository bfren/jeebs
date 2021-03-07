// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Option_Tests
{
	public partial class Operator_Tests
	{
		[Fact]
		public void Equals_When_Equal_Returns_True()
		{
			// Arrange
			var value = F.Rnd.Int;
			var some = Option.Wrap(value);

			// Act
			var result = some == value;

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void Equals_When_Not_Equal_Returns_False()
		{
			// Arrange
			const int v0 = 18;
			const int v1 = 7;
			var some = Option.Wrap(v0);

			// Act
			var result = some == v1;

			// Assert
			Assert.False(result);
		}
	}
}
