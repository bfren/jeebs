// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Messages.Msg_Tests;

public class FormatWithType_Tests
{
	[Fact]
	public void Returns_MsgType_When_Empty()
	{
		// Arrange
		var msg = new TestMsg();

		// Act
		var result = msg.FormatWithType;

		// Assert
		Assert.Equal("{MsgType}", result);
	}

	[Fact]
	public void Prepends_MsgType_To_Format()
	{
		// Arrange
		var format = Rnd.Str;
		var msg = new TestMsg(format);

		// Act
		var result = msg.FormatWithType;

		// Assert
		Assert.Equal($"{{MsgType}} {format}", result);
	}

	[Fact]
	public void Trims_Format()
	{
		// Arrange
		var format = Rnd.Str;
		var msg = new TestMsg($"{format}  ");

		// Act
		var result = msg.FormatWithType;

		// Assert
		Assert.Equal($"{{MsgType}} {format}", result);
	}

	public sealed record class TestMsg : Msg
	{
		public TestMsg() { }

		public TestMsg(string format) =>
			Format = format;
	}
}
