// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;
using MaybeF;

namespace Jeebs.MaybeExtensions_Tests;

public class LogBool_Tests
{
	[Fact]
	public void Is_Some__Value_True__Calls_Log_Inf__With_Correct_Values()
	{
		// Arrange
		var maybe = F.True;
		var done = Rnd.Str;
		var log = Substitute.For<ILog>();

		// Act
		_ = maybe.LogBool(log);
		_ = maybe.LogBool(log, done: done, failed: Rnd.Str);

		// Assert
		log.Received().Inf("Done.");
		log.Received().Inf(done);
	}

	[Fact]
	public void Is_Some__Value_False__Calls_Log_Err__With_Correct_Values()
	{
		// Arrange
		var maybe = F.False;
		var failed = Rnd.Str;
		var log = Substitute.For<ILog>();

		// Act
		_ = maybe.LogBool(log);
		_ = maybe.LogBool(log, done: Rnd.Str, failed: failed);

		// Assert
		log.Received().Err("Failed.");
		log.Received().Err(failed);
	}

	[Fact]
	public void Is_None__Calls_Log_Msg__With_Correct_Values()
	{
		// Arrange
		var msg = Substitute.For<IMsg>();
		var maybe = F.None<bool>(msg);
		var log = Substitute.For<ILog>();

		// Act
		_ = maybe.LogBool(log);
		_ = maybe.LogBool(log, done: Rnd.Str, failed: Rnd.Str);

		// Assert
		log.Received(2).Msg(msg);
	}
}
