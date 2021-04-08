// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static F.OptionF;

namespace Jeebs.Option_Tests
{
	public partial class Operator_Tests
	{
		[Fact]
		public void Equals_When_Equal_Returns_True()
		{
			// Arrange
			var value = F.Rnd.Int;
			var some = Return(value);

			// Act
			var r0 = some == value;
			var r1 = value == some;

			// Assert
			Assert.True(r0);
			Assert.True(r1);
		}

		[Fact]
		public void Equals_When_Not_Equal_Returns_False()
		{
			// Arrange
			var v0 = F.Rnd.Int;
			var v1 = F.Rnd.Int;
			var some = Return(v0);

			// Act
			var r0 = some == v1;
			var r1 = v1 == some;

			// Assert
			Assert.False(r0);
			Assert.False(r1);
		}

		public record TestMsg : IMsg { }
	}
}
