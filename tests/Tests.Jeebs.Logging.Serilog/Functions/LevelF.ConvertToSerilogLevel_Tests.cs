// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Serilog.Events;
using Wrap.Logging;
using static Jeebs.Logging.Serilog.Functions.LevelF;

namespace Jeebs.Logging.Serilog.Functions.LevelF_Tests;

public class ConvertToSerilogLevel_Tests
{
	public class with_valid_input
	{
		[Theory]
		[InlineData(LogLevel.Unknown, LogEventLevel.Verbose)]
		[InlineData(LogLevel.Verbose, LogEventLevel.Verbose)]
		[InlineData(LogLevel.Debug, LogEventLevel.Debug)]
		[InlineData(LogLevel.Information, LogEventLevel.Information)]
		[InlineData(LogLevel.Warning, LogEventLevel.Warning)]
		[InlineData(LogLevel.Error, LogEventLevel.Error)]
		[InlineData(LogLevel.Fatal, LogEventLevel.Fatal)]
		public void returns_correct_level(LogLevel input, LogEventLevel output)
		{
			// Arrange

			// Act
			var result = ConvertToSerilogLevel(input);

			// Assert
			Assert.Equal(output, result);
		}
	}

	public class with_invalid_input
	{
		[Fact]
		public void returns_fail()
		{
			// Arrange
			var input = (LogLevel)(1 << Rnd.NumberF.GetInt32(6, 20));
			var expected = "Unknown LogLevel: {Level}.";

			// Act
			var result = ConvertToSerilogLevel(input);

			// Assert
			result.AssertFailure(expected, input);
		}
	}
}
