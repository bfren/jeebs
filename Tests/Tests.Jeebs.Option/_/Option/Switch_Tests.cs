// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using NSubstitute;
using Xunit;

namespace Jeebs.Option_Tests
{
	public class Switch_Tests
	{
		[Fact]
		public void If_Some_Run_Some()
		{
			// Arrange
			var value = F.Rnd.Int;
			var some = Option.Wrap(value);
			var none = Substitute.For<Action>();

			// Act
			var result = 0;
			some.Switch(
				some: some => result = some,
				none: none
			);

			// Assert
			Assert.Equal(value, result);
			none.DidNotReceive().Invoke();
		}

		[Fact]
		public void If_None_Run_None()
		{
			// Arrange
			var value = F.Rnd.Int;
			var some = Substitute.For<Action<int>>();
			var none = Option.None<int>(true);

			// Act
			var result = 0;
			none.Switch(
				some: some,
				none: () => result = value
			);

			// Assert
			Assert.Equal(value, result);
			some.DidNotReceive().Invoke(Arg.Any<int>());
		}
	}
}
