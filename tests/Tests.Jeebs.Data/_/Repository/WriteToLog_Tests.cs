// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Repository_Tests;

public class WriteToLog_Tests
{
	[Fact]
	public void Sends_Message_And_Args_To_Debug_Log()
	{
		// Arrange
		var (_, log, entity) = Repository_Setup.Get();
		var message = Rnd.Str;
		var args = new object[] { Rnd.Int, Rnd.Int };

		// Act
		entity.WriteToLogTest(message, args);

		// Assert
		log.Received().Dbg(message, args);
	}
}
