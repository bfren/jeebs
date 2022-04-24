// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;
using MaybeF;

namespace Jeebs.MaybeExtensions_Tests;

public class LogBoolAsyncAsync_Tests
{
	[Fact]
	public async Task Is_Some__Value_True__Calls_Log_Inf__With_Correct_Values()
	{
		// Arrange
		var maybe = F.True.AsTask;
		var done = Rnd.Str;
		var log = Substitute.For<ILog>();

		// Act
		_ = await maybe.LogBoolAsync(log);
		_ = await maybe.LogBoolAsync(log, done: done, failed: Rnd.Str);

		// Assert
		log.Received().Inf("Done.");
		log.Received().Inf(done);
	}

	[Fact]
	public async Task Is_Some__Value_False__Calls_Log_Err__With_Correct_Values()
	{
		// Arrange
		var maybe = F.False.AsTask;
		var failed = Rnd.Str;
		var log = Substitute.For<ILog>();

		// Act
		_ = await maybe.LogBoolAsync(log);
		_ = await maybe.LogBoolAsync(log, done: Rnd.Str, failed: failed);

		// Assert
		log.Received().Err("Failed.");
		log.Received().Err(failed);
	}

	[Fact]
	public async Task Is_None__Calls_Log_Msg__With_Correct_Values()
	{
		// Arrange
		var msg = Substitute.For<IMsg>();
		var maybe = F.None<bool>(msg).AsTask;
		var log = Substitute.For<ILog>();

		// Act
		_ = await maybe.LogBoolAsync(log);
		_ = await maybe.LogBoolAsync(log, done: Rnd.Str, failed: Rnd.Str);

		// Assert
		log.Received(2).Msg(msg);
	}
}
