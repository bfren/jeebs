// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using MaybeF;

namespace Jeebs.Mvc.Result_Tests;

public class StatusCode_Tests
{
	[Fact]
	public void StatusCode_Set__Returns_StatusCode()
	{
		// Arrange
		var code = Rnd.Int;
		var jsonResult = Result.Create(F.True) with { StatusCode = code };

		// Act
		var result = jsonResult.StatusCode;

		// Assert
		Assert.Equal(code, result);
	}

	[Fact]
	public void StatusCode_Not_Set__Success_Is_True__Returns_200()
	{
		// Arrange
		var jsonResult = Result.Create(F.True);

		// Act
		var result = jsonResult.StatusCode;

		// Assert
		Assert.Equal(200, result);
	}

	[Fact]
	public void StatusCode_Not_Set__Success_Is_False__Returns_500()
	{
		// Arrange
		var jsonResult = Result.Create(F.False);

		// Act
		var result = jsonResult.StatusCode;

		// Assert
		Assert.Equal(500, result);
	}
}
