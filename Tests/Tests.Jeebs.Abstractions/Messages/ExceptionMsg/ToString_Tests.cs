// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Logging;
using Xunit;

namespace Jeebs.Abstractions.Messages.ExceptionMsg_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void Returns_LogLevel_And_Exception_Message()
		{
			// Arrange
			var value = F.Rnd.Str;
			var ex = new Exception(value);
			var msg = new TestMsg(ex);

			// Act
			var result = msg.ToString();

			// Assert
			Assert.Contains(value, result);
			Assert.Equal($"TestMsg {{ Exception = {ex}, Level = {LogLevel.Fatal} }}", result);
		}

		public record TestMsg : ExceptionMsg
		{
			public override LogLevel Level => LogLevel.Fatal;

			public TestMsg(Exception ex) : base(ex) { }
		}
	}
}
