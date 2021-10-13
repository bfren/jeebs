// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;
using static F.OptionF.Msg;

namespace Jeebs.Option_Tests;

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
		var some = result.AssertSome();
		Assert.Equal(input, some);
	}

	[Theory]
	[InlineData(null)]
	public void Implicit_With_Null_Returns_None(object input)
	{
		// Arrange

		// Act
		Option<object> result = input;

		// Assert
		var none = result.AssertNone();
		Assert.IsType<NullValueMsg>(none);
	}
}
