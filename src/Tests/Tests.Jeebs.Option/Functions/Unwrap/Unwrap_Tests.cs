// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class Unwrap_Tests
	{
		[Fact]
		public void Some_Returns_Value()
		{
			// Arrange
			var value = Rnd.Int;
			var some = Return(value);

			// Act
			var result = Unwrap(some, Substitute.For<Func<IMsg, int>>());

			// Assert
			Assert.Equal(value, result);
		}

		[Fact]
		public void None_Gets_IfNone()
		{
			// Arrange
			var value = Rnd.Int;
			var none = None<int>(true);

			// Act
			var result = Unwrap(none, _ => value);

			// Assert
			Assert.Equal(value, result);
		}

		[Fact]
		public void None_Runs_IfNone()
		{
			// Arrange
			var none = None<int>(true);
			var ifNone = Substitute.For<Func<IMsg, int>>();

			// Act
			Unwrap(none, ifNone);

			// Assert
			ifNone.ReceivedWithAnyArgs().Invoke(Arg.Any<IMsg>());
		}

		[Fact]
		public void None_With_Reason_Runs_IfNone_Passes_Reason()
		{
			// Arrange
			var msg = Substitute.For<IMsg>();
			var none = None<int>(msg);
			var ifNone = Substitute.For<Func<IMsg, int>>();

			// Act
			Unwrap(none, ifNone);

			// Assert
			ifNone.Received().Invoke(msg);
		}
	}
}
