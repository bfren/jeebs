// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace Jeebs.Messages.Msg_Tests;

public class DefaultLevel_Tests
{
	[Fact]
	public void Returns_Information()
	{
		// Arrange

		// Act
		var result = Msg.DefaultLevel;

		// Assert
		Assert.Equal(LogLevel.Information, result);
	}
}
