// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Enums;

namespace Jeebs.Mvc.Op_Tests;

public class Error_Tests
{
	[Fact]
	public void With_Message__Sets_Alert_Error()
	{
		// Arrange
		var value = Rnd.Str;

		// Act
		var result = Op.Error(value);

		// Assert
		var op = Assert.IsType<Op<bool>>(result);
		Assert.False(op.Value);
		Assert.Equal(AlertType.Error, op.Message.Type);
		Assert.Equal(value, op.Message.Text);
	}

	[Fact]
	public void With_Failure__Sets_Alert_Error()
	{
		// Arrange
		var failure = FailGen.Create().Value;

		// Act
		var result = Op.Error(failure);

		// Assert
		var op = Assert.IsType<Op<bool>>(result);
		Assert.False(op.Value);
		Assert.Equal(AlertType.Error, op.Message.Type);
		Assert.Equal(failure.ToString(), op.Message.Text);
	}
}
