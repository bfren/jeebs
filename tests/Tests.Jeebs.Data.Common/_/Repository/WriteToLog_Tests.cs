// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Common.Repository_Tests;

public class WriteToLog_Tests : Repository_Setup
{
	[Fact]
	public void Sends_Message_And_Args_To_Verbose_Log()
	{
		// Arrange
		var (repo, v) = Setup();
		var message = Rnd.Str;
		var args = new object[] { Rnd.Int, Rnd.Int };

		// Act
		repo.WriteToLogTest(message, args);

		// Assert
		v.Log.Received().Vrb(message, args);
	}
}
