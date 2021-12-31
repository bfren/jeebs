// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Msg_Tests;

public class FormatWithType_Tests
{
	[Fact]
	public void Adds_MsgType()
	{
		// Arrange
		var format = F.Rnd.Str;
		var msg = new TestMsg(format);

		// Act
		var result = msg.FormatWithType;

		// Assert
		Assert.Equal($"{{MsgType}} {format}", result);
	}

	public record class TestMsg : Msg
	{
		public TestMsg(string format) =>
			Format = format;
	}
}
