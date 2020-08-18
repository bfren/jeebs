using System;
using System.Collections.Generic;
using System.Text;
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
			const int value = 18;

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
			const int value = 18;

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
