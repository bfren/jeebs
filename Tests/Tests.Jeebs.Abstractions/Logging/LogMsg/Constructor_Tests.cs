// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using NSubstitute;
using Xunit;

namespace Jeebs.Logging.LogMsg_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Without_Level_Uses_Default_Level_Information()
		{
			// Arrange

			// Act
			var result = new TestMsg(F.Rnd.Str);

			// Assert
			Assert.Equal(LogLevel.Information, result.Level);
		}

		[Fact]
		public void With_Level_Sets_Level()
		{
			// Arrange
			const LogLevel value = LogLevel.Fatal;

			// Act
			var result = new TestMsg(value, F.Rnd.Str);

			// Assert
			Assert.Equal(value, result.Level);
		}

		[Fact]
		public void Prepends_MsgType_To_Format()
		{
			// Arrange
			var value = F.Rnd.Str;

			// Act
			var result = new TestMsg(value);

			// Assert
			Assert.Equal("{MsgType} " + value, result.Format);
		}

		public record TestMsg : LogMsg
		{
			public override Func<object[]> Args =>
				() => Array.Empty<object>();

			public TestMsg(string format) : base(format) { }
			public TestMsg(LogLevel level, string format) : base(level, format) { }
		}
	}
}
