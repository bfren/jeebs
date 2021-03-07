// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using NSubstitute;
using Xunit;

namespace Jeebs.Option_Tests
{
	public class WrapIf_Tests
	{
		[Fact]
		public void True_Returns_Some()
		{
			// Arrange
			var value = F.Rnd.Int;

			// Act
			var result = Option.WrapIf(() => true, () => value);

			// Assert
			var some = Assert.IsType<Some<int>>(result);
			Assert.Equal(value, some.Value);
		}

		[Fact]
		public void False_Returns_None()
		{
			// Arrange
			var value = F.Rnd.Int;

			// Act
			var result = Option.WrapIf(() => false, () => value);

			// Assert
			Assert.IsType<None<int>>(result);
		}

		[Fact]
		public void False_Bypasses_Value_Func()
		{
			// Arrange
			var getValue = Substitute.For<Func<int>>();

			// Act
			var result = Option.WrapIf(() => false, getValue);

			// Assert
			Assert.IsType<None<int>>(result);
			getValue.DidNotReceive().Invoke();
		}
	}
}
