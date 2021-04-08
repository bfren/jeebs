// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;
using static F.OptionF;

namespace Jeebs.None_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void When_Not_IExceptionMsg_Returns_Reason_ToString()
		{
			// Arrange
			var value = F.Rnd.Str;
			var msg = new TestMsg(value);
			var option = None<int>(msg);

			// Act
			var result = option.ToString();

			// Assert
			Assert.Equal($"{nameof(TestMsg)}: {value}", result);
		}

		[Fact]
		public void When_IExceptionMsg_Returns_Msg_Type_And_Exception_Message()
		{
			// Arrange
			var value = F.Rnd.Str;
			var exception = new Exception(value);
			var option = None<int, TestExceptionMsg>(exception);

			// Act
			var result = option.ToString();

			// Assert
			Assert.Equal($"{typeof(TestExceptionMsg)}: {value}", result);
		}

		public record TestMsg(string Value) : IMsg
		{
			public override string ToString() =>
				$"{nameof(TestMsg)}: {Value}";
		}

		public record TestExceptionMsg() : IExceptionMsg
		{
			public Exception Exception { get; init; } = new();
		}
	}
}
