// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace JeebsF.Option_Tests
{
	public partial class Operator_Tests
	{
		[Theory]
		[InlineData(18)]
		[InlineData("foo")]
		public void Implicit_With_Value_Returns_Some<T>(T input)
		{
			// Arrange

			// Act
			Option<T> result = input;

			// Assert
			var some = Assert.IsType<Some<T>>(result);
			Assert.Equal(input, some.Value);
		}

		[Theory]
		[InlineData(null)]
		public void Implicit_With_Null_Returns_None(object input)
		{
			// Arrange

			// Act
			Option<object> result = input;

			// Assert
			var none = Assert.IsType<None<object>>(result);
			Assert.IsType<Jm.Option.SomeValueWasNullMsg>(none.Reason);
		}
	}
}
