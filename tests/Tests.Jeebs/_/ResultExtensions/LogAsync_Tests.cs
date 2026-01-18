// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace Jeebs.ResultExtensions_Tests;

public class LogAsync_Tests
{
	[Fact]
	public async Task Is_Ok__Calls_Log_Inf__With_Correct_Values()
	{
		// Arrange
		var value = Rnd.Str;
		var result = R.Wrap(value).AsTask();
		var message = Rnd.Str;
		var log = Substitute.For<ILog>();

		// Act
		_ = result.LogAsync(log);
		_ = result.LogAsync(log, message);

		// Assert
		log.Received().Inf("Done: {Value}.", value);
		log.Received().Inf(message, value);
	}

	[Fact]
	public async Task Is_Fail__Calls_Log_Failure__With_Correct_Values()
	{
		// Arrange
		var failure = R.Fail<bool>(nameof(Log_Tests), nameof(Is_Fail__Calls_Log_Failure__With_Correct_Values));
		Result<bool> fail() => failure;
		var result = fail().AsTask();
		var log = Substitute.For<ILog>();

		// Act
		_ = result.LogAsync(log);
		_ = result.LogAsync(log, message: Rnd.Str);

		// Assert
		log.Received(2).Failure(failure.Value);
	}
}
