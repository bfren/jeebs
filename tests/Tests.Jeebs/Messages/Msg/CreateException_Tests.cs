// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Messages.Exceptions;
using MaybeF;

namespace Jeebs.Messages.Msg_Tests;

public class CreateException_Tests
{
	[Fact]
	public void Creates_MsgException_With_Msg()
	{
		// Arrange
		var msg = Substitute.For<IMsg>();

		// Act
		var result = Msg.CreateException(msg);

		// Assert
		Assert.IsType<MsgException<IMsg>>(result);
	}
}
