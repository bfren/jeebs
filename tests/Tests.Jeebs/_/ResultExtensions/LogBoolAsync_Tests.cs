// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace Jeebs.ResultExtensions_Tests;

public class LogBoolAsyncAsync_Tests
{
	[Fact]
	public async Task Is_Ok__Value_True__Calls_Log_Inf__With_Correct_Values()
	{
		// Arrange
		var result = R.True.AsTask();
		var done = Rnd.Str;
		var log = Substitute.For<ILog>();

		// Act
		_ = await result.LogBoolAsync(log);
		_ = await result.LogBoolAsync(log, ifTrue: done, ifFalse: Rnd.Str);

		// Assert
		log.Received().Inf("Done.");
		log.Received().Inf(done);
	}

	[Fact]
	public async Task Is_Ok__Value_False__Calls_Log_Err__With_Correct_Values()
	{
		// Arrange
		var result = R.False.AsTask();
		var failed = Rnd.Str;
		var log = Substitute.For<ILog>();

		// Act
		_ = await result.LogBoolAsync(log);
		_ = await result.LogBoolAsync(log, ifTrue: Rnd.Str, ifFalse: failed);

		// Assert
		log.Received().Err("Failed.");
		log.Received().Err(failed);
	}

	[Fact]
	public async Task Is_Fail__Calls_Log_Failure__With_Correct_Values()
	{
		// Arrange
		var failure = R.Fail<bool>(nameof(LogBoolAsyncAsync_Tests), nameof(Is_Fail__Calls_Log_Failure__With_Correct_Values));
		Result<bool> fail() => failure;
		var result = fail().AsTask();
		var log = Substitute.For<ILog>();

		// Act
		_ = await result.LogBoolAsync(log);
		_ = await result.LogBoolAsync(log, ifTrue: Rnd.Str, ifFalse: Rnd.Str);

		// Assert
		log.Received(2).Failure(failure.Value);
	}
}
