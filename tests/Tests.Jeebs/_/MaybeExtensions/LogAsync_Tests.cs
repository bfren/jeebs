// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;
using MaybeF;

namespace Jeebs.MaybeExtensions_Tests;

public class LogAsync_Tests
{
	[Fact]
	public async Task Is_Some__Calls_Log_Inf__With_Correct_Values()
	{
		// Arrange
		var value = Rnd.Str;
		var maybe = F.Some(value).AsTask;
		var message = Rnd.Str;
		var log = Substitute.For<ILog>();

		// Act
		_ = await maybe.LogAsync(log);
		_ = await maybe.LogAsync(log, message);

		// Assert
		log.Received().Inf("Done: {Value}.", value);
		log.Received().Inf(message, value);
	}

	[Fact]
	public async Task Is_None__Calls_Log_Msg__With_Correct_Values()
	{
		// Arrange
		var msg = Substitute.For<IMsg>();
		var maybe = F.None<bool>(msg).AsTask;
		var log = Substitute.For<ILog>();

		// Act
		_ = await maybe.LogAsync(log);
		_ = await maybe.LogAsync(log, message: Rnd.Str);

		// Assert
		log.Received(2).Msg(msg);
	}
}
