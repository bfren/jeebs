// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace Jeebs.ResultExtensions_Tests;

public class LogBool_Tests
{
	[Fact]
	public void Is_Ok__Value_True__Calls_Log_Inf__With_Correct_Values()
	{
		// Arrange
		var result = R.True;
		var done = Rnd.Str;
		var log = Substitute.For<ILog>();

		// Act
		_ = result.LogBool(log);
		_ = result.LogBool(log, ifTrue: done, ifFalse: Rnd.Str);

		// Assert
		log.Received().Inf("Done.");
		log.Received().Inf(done);
	}

	[Fact]
	public void Is_Ok__Value_False__Calls_Log_Err__With_Correct_Values()
	{
		// Arrange
		var result = R.False;
		var failed = Rnd.Str;
		var log = Substitute.For<ILog>();

		// Act
		_ = result.LogBool(log);
		_ = result.LogBool(log, ifTrue: Rnd.Str, ifFalse: failed);

		// Assert
		log.Received().Err("Failed.");
		log.Received().Err(failed);
	}

	[Fact]
	public void Is_Fail__Calls_Log_Failure__With_Correct_Values()
	{
		// Arrange
		var failure = FailGenerator.Create();
		Result<bool> fail() => failure;
		var result = fail();
		var log = Substitute.For<ILog>();

		// Act
		_ = result.LogBool(log);
		_ = result.LogBool(log, ifTrue: Rnd.Str, ifFalse: Rnd.Str);

		// Assert
		log.Received(2).Failure(failure.Value);
	}
}
