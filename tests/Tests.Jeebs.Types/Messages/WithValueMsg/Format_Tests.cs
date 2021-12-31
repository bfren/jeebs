// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.WithValueMsg_Tests;

public class Format_Tests
{
	[Fact]
	public void Includes_Value_Tag()
	{
		// Arrange
		var msg = new TestMsg(F.Rnd.Str);

		// Act
		var result = msg.Format;

		// Assert
		Assert.Equal("{{ Value = {Value} }}", result);
	}

	[Fact]
	public void Uses_Name()
	{
		// Arrange
		var name = F.Rnd.Str;
		var msg = new TestMsg(name, F.Rnd.Str);

		// Act
		var result = msg.Format;

		// Assert
		Assert.Equal("{{ " + name + " = {Value} }}", result);
	}

	public record class TestMsg(string Value) : WithValueMsg<string>
	{
		public TestMsg(string name, string value) : this(value) =>
			Name = name;
	}
}
