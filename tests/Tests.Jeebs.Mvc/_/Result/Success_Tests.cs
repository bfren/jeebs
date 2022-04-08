// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Mvc.Result_Tests;

public class Success_Tests
{
	[Fact]
	public void Type_Is_Boolean__Maybe_Is_Some__Returns_Value()
	{
		// Arrange
		var value = Rnd.Flip;
		var jsonResult = new Result<bool>(value);

		// Act
		var result = jsonResult.Success;

		// Assert
		Assert.Equal(value, result);
	}

	[Fact]
	public void Type_Is_Not_Boolean__Maybe_Is_Some__Returns_True()
	{
		// Arrange
		var jsonResult = new Result<string>(Rnd.Str);

		// Act
		var result = jsonResult.Success;

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void Maybe_Is_None__Returns_False()
	{
		// Arrange
		var maybe = Create.None<string>();
		var jsonResult = new Result<string>(maybe);

		// Act
		var result = jsonResult.Success;

		// Assert
		Assert.False(result);
	}
}
