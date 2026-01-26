// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace Jeebs.ResultExtensions_Tests;

public class Log_Tests
{
	[Fact]
	public void Is_Ok__Calls_Log_Inf__With_Correct_Values()
	{
		// Arrange
		var value = Rnd.Str;
		var result = R.Wrap(value);
		var message = Rnd.Str;
		var log = Substitute.For<ILog>();

		// Act
		_ = result.Log(log);
		_ = result.Log(log, message);

		// Assert
		log.Received().Inf("Done: {Value}.", value);
		log.Received().Inf(message, value);
	}

	[Fact]
	public void Is_Fail__Calls_Log_Failure__With_Correct_Values()
	{
		// Arrange
		var failure = FailGen.Create();
		Result<bool> fail() => failure;
		var result = fail();
		var log = Substitute.For<ILog>();

		// Act
		_ = result.Log(log);
		_ = result.Log(log, message: Rnd.Str);

		// Assert
		log.Received(2).Failure(failure.Value);
	}
}
