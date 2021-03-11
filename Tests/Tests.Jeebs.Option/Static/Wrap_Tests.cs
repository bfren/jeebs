// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.OptionStatic_Tests
{
	public class Wrap_Tests
	{
		[Theory]
		[InlineData(18)]
		[InlineData("foo")]
		public void Value_Input_Returns_Some<T>(T input)
		{
			// Arrange

			// Act
			var result = Option.Wrap(input);

			// Assert
			var some = Assert.IsType<Some<T>>(result);
			Assert.Equal(input, some.Value);
		}

		[Fact]
		public void Null_Input_Returns_None()
		{
			// Arrange
			int? value = null;

			// Act
			var result = Option.Wrap(value);

			// Assert
			var none = Assert.IsType<None<int?>>(result);
			Assert.True(none.Reason is Jm.Option.SomeValueWasNullMsg);
		}

		[Fact]
		public void Null_Input_Returns_Some_If_AllowNull_Is_True()
		{
			// Arrange
			int? value = null;

			// Act
			var result = Option.Wrap(value, true);

			// Assert
			var some = Assert.IsType<Some<int?>>(result);
			Assert.Null(some.Value);
		}

		[Fact]
		public void Null_Input_Allow_Nulls_Returns_Some_With_Null_Value()
		{
			// Arrange
			int? value = null;

			// Act
			var result = Option.Wrap(value, true);

			// Assert
			var some = Assert.IsType<Some<int?>>(result);
			Assert.Null(some.Value);
		}
	}
}
