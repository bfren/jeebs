// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Enums;
using MaybeF;

namespace Jeebs.Mvc.Result_Tests;
public class Error_Tests
{
	[Fact]
	public void With_Message__Sets_Alert_Error()
	{
		// Arrange
		var value = Rnd.Str;

		// Act
		var result = Result.Error(value);

		// Assert
		var some = Assert.IsType<Result<bool>>(result);
		Assert.False(some.Value);
		Assert.Equal(AlertType.Error, some.Message.Type);
		Assert.Equal(value, some.Message.Text);
	}

	[Fact]
	public void With_Reason__Sets_Maybe()
	{
		// Arrange
		var msg = Substitute.For<IMsg>();

		// Act
		var result = Result.Error(msg);

		// Assert
		var some = Assert.IsType<Result<bool>>(result);
		Assert.False(some.Value);
		Assert.Equal(AlertType.Error, some.Message.Type);
		Assert.Equal(msg.ToString(), some.Message.Text);
	}
}
