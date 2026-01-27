// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Enums;
using Jeebs.Mvc.Models;

namespace Jeebs.Mvc.Op_Tests;

public class Message_Tests
{
	[Fact]
	public void Message_Set__Returns_Message()
	{
		// Arrange
		var message = Alert.Info(Rnd.Str);
		var jsonResult = (Op<int>)Op.Create(Rnd.Int, message);

		// Act
		var result = jsonResult.Message;

		// Assert
		Assert.Same(message, result);
	}

	[Fact]
	public void Result_Is_Ok__Returns_Success()
	{
		// Arrange
		var jsonResult = new Op<Guid>(Rnd.Guid);

		// Act
		var result = jsonResult.Message;

		// Assert
		Assert.Equal(AlertType.Success, result.Type);
		Assert.Equal(nameof(AlertType.Success), result.Text);
	}

	[Fact]
	public void Result_Is_Failure__Returns_Error()
	{
		// Arrange
		var failure = FailGen.Create().Value;
		var fail = R.Fail(failure);
		var jsonResult = new Op<long>(fail);

		// Act
		var result = jsonResult.Message;

		// Assert
		Assert.Equal(AlertType.Error, result.Type);
		Assert.Equal(failure.ToString(), result.Text);
	}
}
