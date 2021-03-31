// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;
using Xunit;

namespace Jeebs.Data.DbQuery_Tests
{
	public class WriteToLog_Tests
	{
		[Fact]
		public void Sends_Message_And_Args_To_Debug_Log()
		{
			// Arrange
			var (_, _, log, query) = DbQuery_Setup.Get();
			var message = F.Rnd.Str;
			var args = new object[] { F.Rnd.Int, F.Rnd.Int };

			// Act
			query.WriteToLogTest(message, args);

			// Assert
			log.Received().Debug(message, args);
		}
	}
}
