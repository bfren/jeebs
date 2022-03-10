// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Messages.Msg_Tests;

public class ToString_Tests
{
	[Fact]
	public void Empty_Format_Returns_Type_Name()
	{
		// Arrange
		var msg = new TestMsg();

		// Act
		var result = msg.ToString();

		// Assert
		Assert.Equal(typeof(TestMsg).ToString(), result);
	}

	[Fact]
	public void Returns_Formatted_String()
	{
		// Arrange
		var format = "{0} = {1}";
		var a0 = Rnd.Str;
		var a1 = Rnd.Int;
		var msg = new TestMsg(format, a0, a1);
		var expected = $"{typeof(TestMsg)} {a0} = {a1}";

		// Act
		var result = msg.ToString();

		// Assert
		Assert.Equal(expected, result);
	}

	public sealed record class TestMsg : Msg
	{
		public TestMsg() { }

		public TestMsg(string format, params object[] args) =>
			(Format, Args) = (format, args);
	}
}
