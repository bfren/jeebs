// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Option_Tests
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

		[Theory]
		[InlineData(null)]
		public void Null_Input_Returns_None(object input)
		{
			// Arrange

			// Act
			var result = Option.Wrap(input);

			// Assert
			var none = Assert.IsType<None<object>>(result);
			Assert.True(none.Reason is Jm.Option.SomeValueWasNullMsg);
		}
	}
}
