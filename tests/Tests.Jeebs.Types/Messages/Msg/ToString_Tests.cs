// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Msg_Tests;

public class ToString_Tests
{
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public void Without_Format_Returns_Msg_Type(string input)
	{
		// Arrange
		var msg = new TestMsg(input);

		// Act
		var result = msg.ToString();

		// Assert
		Assert.Equal(msg.GetType().ToString(), result);
	}

	[Fact]
	public void With_Format_Returns_Formatted_String()
	{
		// Arrange
		var format = "{0} - {1}";
		var v0 = F.Rnd.Str;
		var v1 = F.Rnd.Int;
		var msg = new TestMsg(format, new object[] { v0, v1 });

		// Act
		var result = msg.ToString();

		// Assert
		Assert.Equal($"{typeof(TestMsg)} {v0} - {v1}", result);
	}

	public record class TestMsg : Msg
	{
		public TestMsg(string format) =>
			Format = format;

		public TestMsg(string format, object[] args) =>
			(Format, Args) = (format, args);
	}
}
