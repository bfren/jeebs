// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Mvc.Result_Tests;
public class Redirect_Tests
{
	[Fact]
	public void Sets_Value_And_Redirect()
	{
		// Arrange
		var value = Rnd.Str;

		// Act
		var result = Result.Redirect(value);

		// Assert
		var some = Assert.IsType<Result<bool>>(result);
		Assert.True(some.Success);
		Assert.Equal(value, some.RedirectTo);
	}
}
