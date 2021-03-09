// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using NSubstitute;
using Xunit;

namespace Jeebs.Option_Tests
{
	public class Unwrap_Tests
	{
		[Fact]
		public void Some_Returns_Value()
		{
			// Arrange
			var value = F.Rnd.Int;
			var some = Option.Wrap(value);
			var ifNone = Substitute.For<Func<int>>();

			// Act
			var result = some.Unwrap(ifNone);

			// Assert
			Assert.Equal(value, result);
			ifNone.DidNotReceive().Invoke();
		}

		[Fact]
		public void None_Runs_IfNone()
		{
			// Arrange
			var none = Option.None<int>(true);
			var ifNone = Substitute.For<Func<int>>();

			// Act
			var result = none.Unwrap(ifNone);

			// Assert
			ifNone.Received().Invoke();
		}
	}
}
