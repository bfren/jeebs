// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Messages.Exceptions;
using Maybe;

namespace Jeebs.Messages.MsgError_Tests;

public class CreateException_Tests
{
	[Fact]
	public void Creates_MsgException_With_Msg()
	{
		// Arrange
		var msg = Substitute.For<IMsg>();

		// Act
		var result = MsgError.CreateException(msg);

		// Assert
		Assert.IsType<MsgException<IMsg>>(result);
	}

	[Fact]
	public void Creates_MsgException_With_ReasonMsg()
	{
		// Arrange
		var reason = Substitute.For<IReason>();

		// Act
		var result = MsgError.CreateException(reason);

		// Assert
		var msg = Assert.IsType<MsgException<ReasonMsg>>(result);
		Assert.Equal(reason.ToString(), msg.Message);
	}
}
