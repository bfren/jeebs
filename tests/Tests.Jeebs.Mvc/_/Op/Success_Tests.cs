// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Mvc.Op_Tests;

public class Success_Tests
{
	[Fact]
	public void Type_Is_Boolean__Result_Is_Ok__Returns_Value()
	{
		// Arrange
		var value = Rnd.Flip;
		var jsonResult = new Op<bool>(value);

		// Act
		var result = jsonResult.Success;

		// Assert
		Assert.Equal(value, result);
	}

	[Fact]
	public void Type_Is_Not_Boolean__Result_Is_Ok__Returns_True()
	{
		// Arrange
		var jsonResult = new Op<string>(Rnd.Str);

		// Act
		var result = jsonResult.Success;

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void Result_Is_Fail__Returns_False()
	{
		// Arrange
		var fail = FailGen.Create();
		var jsonResult = new Op<string>(fail);

		// Act
		var result = jsonResult.Success;

		// Assert
		Assert.False(result);
	}
}
