// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace JeebsF.Option_Tests
{
	public partial class Operator_Tests
	{
		[Fact]
		public void DoesNotEqual_When_Equal_Returns_False()
		{
			// Arrange
			var value = JeebsF.Rnd.Int;
			var some = OptionF.Return(value);

			// Act
			var result = some != value;

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void DoesNotEqual_When_Not_Equal_Returns_True()
		{
			// Arrange
			var v0 = JeebsF.Rnd.Int;
			var v1 = JeebsF.Rnd.Int;
			var some = OptionF.Return(v0);

			// Act
			var result = some != v1;

			// Assert
			Assert.True(result);
		}
	}
}
