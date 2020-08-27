using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Log_Tests
{
	public class Message_Tests
	{
		[Fact]
		public void Msg_Runs_Debug()
		{
			// Arrange
			var msg = Substitute.For<IMsg>();
			var log = Substitute.For<Log>();

			// Act
			log.Message(msg);

			// Assert
			log.Received().Debug(Arg.Any<string>(), Arg.Any<object[]>());
		}

		[Fact]
		public void LoggableMsg_Trace_Runs_Trace()
		{
			// Arrange
			var msg = Substitute.For<ILoggableMsg>();
			msg.Level.Returns(Microsoft.Extensions.Logging.LogLevel.Trace);
			var log = Substitute.For<Log>();

			// Act
			log.Message(msg);

			// Assert
			log.Received().Trace(Arg.Any<string>(), Arg.Any<object[]>());
		}

		[Fact]
		public void LoggableMsg_Debug_Runs_Debug()
		{
			// Arrange
			var msg = Substitute.For<ILoggableMsg>();
			msg.Level.Returns(Microsoft.Extensions.Logging.LogLevel.Debug);
			var log = Substitute.For<Log>();

			// Act
			log.Message(msg);

			// Assert
			log.Received().Debug(Arg.Any<string>(), Arg.Any<object[]>());
		}

		[Fact]
		public void LoggableMsg_Information_Runs_Information()
		{
			// Arrange
			var msg = Substitute.For<ILoggableMsg>();
			msg.Level.Returns(Microsoft.Extensions.Logging.LogLevel.Information);
			var log = Substitute.For<Log>();

			// Act
			log.Message(msg);

			// Assert
			log.Received().Information(Arg.Any<string>(), Arg.Any<object[]>());
		}

		[Fact]
		public void LoggableMsg_Warning_Runs_Warning()
		{
			// Arrange
			var msg = Substitute.For<ILoggableMsg>();
			msg.Level.Returns(Microsoft.Extensions.Logging.LogLevel.Warning);
			var log = Substitute.For<Log>();

			// Act
			log.Message(msg);

			// Assert
			log.Received().Warning(Arg.Any<string>(), Arg.Any<object[]>());
		}

		[Fact]
		public void LoggableMsg_Error_Runs_Error()
		{
			// Arrange
			var msg = Substitute.For<ILoggableMsg>();
			msg.Level.Returns(Microsoft.Extensions.Logging.LogLevel.Error);
			var log = Substitute.For<Log>();

			// Act
			log.Message(msg);

			// Assert
			log.Received().Error(Arg.Any<string>(), Arg.Any<object[]>());
		}

		[Fact]
		public void LoggableMsg_Critical_Runs_Critical()
		{
			// Arrange
			var msg = Substitute.For<ILoggableMsg>();
			msg.Level.Returns(Microsoft.Extensions.Logging.LogLevel.Critical);
			var log = Substitute.For<Log>();

			// Act
			log.Message(msg);

			// Assert
			log.Received().Critical(Arg.Any<string>(), Arg.Any<object[]>());
		}

		[Fact]
		public void ExceptionMsg_Error_Runs_Error()
		{
			// Arrange
			var msg = Substitute.For<IExceptionMsg>();
			msg.Level.Returns(Microsoft.Extensions.Logging.LogLevel.Error);
			var log = Substitute.For<Log>();

			// Act
			log.Message(msg);

			// Assert
			log.Received().Error(Arg.Any<Exception>(), Arg.Any<string>(), Arg.Any<object[]>());
		}
		[Fact]
		public void ExceptionMsg_Critical_Runs_Critical()
		{
			// Arrange
			var msg = Substitute.For<IExceptionMsg>();
			msg.Level.Returns(Microsoft.Extensions.Logging.LogLevel.Critical);
			var log = Substitute.For<Log>();

			// Act
			log.Message(msg);

			// Assert
			log.Received().Critical(Arg.Any<Exception>(), Arg.Any<string>(), Arg.Any<object[]>());
		}
	}
}
