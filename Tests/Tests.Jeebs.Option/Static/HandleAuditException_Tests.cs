// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.IO;
using NSubstitute;
using Xunit;

namespace Jeebs.OptionStatic_Tests
{
	public class HandleAuditException_Tests
	{
		[Fact]
		public void If_LogAuditExceptions_Is_Not_Null_Calls_With_Exception()
		{
			// Arrange
			var log = Substitute.For<Action<Exception>>();
			var exception = new Exception();

			// Act
			Option.HandleAuditException(exception, log, Console.Out);

			// Assert
			log.Received().Invoke(exception);
		}

		[Fact]
		public void If_LogAuditExceptions_Is_Null_Writes_To_Console()
		{
			// Arrange
			var message = F.Rnd.Str;
			var exception = new Exception(message);
			var writer = Substitute.For<TextWriter>();

			// Act
			Option.HandleAuditException(exception, null, writer);

			// Assert
			writer.Received().WriteLine("Audit Error: {0}", exception);
		}
	}
}
