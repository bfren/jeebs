// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;

namespace JeebsF.Option_Tests
{
	public class Unwrap_Tests
	{
		[Fact]
		public void Some_Returns_Value()
		{
			// Arrange
			var value = JeebsF.Rnd.Int;
			var some = OptionF.Return(value);

			// Act
			var r0 = some.Unwrap(JeebsF.Rnd.Int);
			var r1 = some.Unwrap(Substitute.For<Func<int>>());
			var r2 = some.Unwrap(Substitute.For<Func<IMsg?, int>>());

			// Assert
			Assert.Equal(value, r0);
			Assert.Equal(value, r1);
			Assert.Equal(value, r2);
		}

		[Fact]
		public void None_Gets_IfNone()
		{
			// Arrange
			var value = JeebsF.Rnd.Int;
			var none = OptionF.None<int>(true);

			// Act
			var result = none.Unwrap(value);

			// Assert
			Assert.Equal(value, result);
		}

		[Fact]
		public void None_Runs_IfNone()
		{
			// Arrange
			var none = OptionF.None<int>(true);
			var ifNone = Substitute.For<Func<IMsg?, int>>();

			// Act
			none.Unwrap(() => ifNone(null));
			none.Unwrap(ifNone);

			// Assert
			ifNone.ReceivedWithAnyArgs(2).Invoke(Arg.Any<IMsg?>());
		}
	}
}
