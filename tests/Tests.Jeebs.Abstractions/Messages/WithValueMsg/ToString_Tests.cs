﻿// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.Abstractions.Messages.WithValueMsg_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void Returns_Value()
		{
			// Arrange
			var value = F.Rnd.Str;
			var msg = new TestMsg(value);

			// Act
			var result = msg.ToString();

			// Assert
			Assert.Contains(value, result);
			Assert.Equal($"TestMsg {{ Value = {value} }}", result);
		}

		public record class TestMsg(string Value) : WithValueMsg<string> { }
	}
}
