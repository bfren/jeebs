// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.DbQuery_Tests
{
	public class WriteToLog_Tests
	{
		[Fact]
		public void Sends_Message_And_Args_To_Verbose_Log()
		{
			// Arrange
			var db = Substitute.For<IDb>();
			var log = Substitute.For<ILog>();
			var query = Substitute.ForPartsOf<DbQuery>(db, log);
			var message = F.Rnd.Str;
			var args = new object[] { F.Rnd.Int, F.Rnd.Int };

			// Act
			query.WriteToLogTest(message, args);

			// Assert
			log.Received().Verbose(message, args);
		}
	}
}
