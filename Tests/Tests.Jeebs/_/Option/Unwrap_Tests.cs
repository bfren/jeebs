using System;
using System.Collections.Generic;
using System.Text;
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
			const int value = 18;
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
			var none = Option.None<int>();
			var ifNone = Substitute.For<Func<int>>();

			// Act
			var result = none.Unwrap(ifNone);

			// Assert
			ifNone.Received().Invoke();
		}
	}
}
