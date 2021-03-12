// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using NSubstitute;
using Xunit;

namespace JeebsF.OptionStatic_Tests
{
	public class WrapIf_Tests
	{
		[Fact]
		public void True_Returns_Some()
		{
			// Arrange
			var value = JeebsF.Rnd.Int;

			// Act
			var r0 = OptionF.ReturnIf(() => true, value);
			var r1 = OptionF.ReturnIf(() => true, () => value);

			// Assert
			var s0 = Assert.IsType<Some<int>>(r0);
			Assert.Equal(value, s0.Value);
			var s1 = Assert.IsType<Some<int>>(r1);
			Assert.Equal(value, s1.Value);
		}

		[Fact]
		public void False_Returns_None()
		{
			// Arrange
			var value = JeebsF.Rnd.Int;

			// Act
			var result = OptionF.ReturnIf(() => false, () => value);

			// Assert
			Assert.IsType<None<int>>(result);
		}

		[Fact]
		public void False_Bypasses_Value_Func()
		{
			// Arrange
			var getValue = Substitute.For<Func<int>>();

			// Act
			var result = OptionF.ReturnIf(() => false, getValue);

			// Assert
			Assert.IsType<None<int>>(result);
			getValue.DidNotReceive().Invoke();
		}
	}
}
