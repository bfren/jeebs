// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Mvc.Enums;
using Jeebs.Mvc.Models;
using MaybeF;

namespace Jeebs.Mvc.Result_Tests;

public class Message_Tests
{
	[Fact]
	public void Message_Set__Returns_Message()
	{
		// Arrange
		var message = Alert.Info(Rnd.Str);
		var jsonResult = (IResult<int>)Result.Create(Rnd.Int, message);

		// Act
		var result = jsonResult.Message;

		// Assert
		Assert.Same(message, result);
	}

	[Fact]
	public void Maybe_Is_Some__Returns_Success()
	{
		// Arrange
		var jsonResult = new Result<Guid>(Rnd.Guid);

		// Act
		var result = jsonResult.Message;

		// Assert
		Assert.Equal(AlertType.Success, result.Type);
		Assert.Equal(nameof(AlertType.Success), result.Text);
	}

	[Fact]
	public void Maybe_Is_None__Returns_Reason()
	{
		// Arrange
		var msg = Substitute.For<IMsg>();
		var maybe = F.None<long>(msg);
		var jsonResult = new Result<long>(maybe);

		// Act
		var result = jsonResult.Message;

		// Assert
		Assert.Equal(AlertType.Error, result.Type);
		Assert.Same(msg.ToString(), result.Text);
	}
}
