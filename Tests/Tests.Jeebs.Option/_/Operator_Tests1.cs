// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static JeebsF.OptionF;

namespace Jeebs.Option_Tests
{
	public partial class Operator_Tests
	{
		[Fact]
		public void Equals_When_Equal_Returns_True()
		{
			// Arrange
			var value = JeebsF.Rnd.Int;
			var some = Return(value);

			// Act
			var result = some == value;

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void Equals_When_Not_Equal_Returns_False()
		{
			// Arrange
			var v0 = JeebsF.Rnd.Int;
			var v1 = JeebsF.Rnd.Int;
			var some = Return(v0);

			// Act
			var result = some == v1;

			// Assert
			Assert.False(result);
		}

		public class TestMsg : IMsg { }
	}
}
