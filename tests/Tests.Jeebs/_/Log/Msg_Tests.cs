// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;
using Jeebs.Messages;

namespace Jeebs.Log_Tests;

public class Msg_Tests
{
	[Fact]
	public void Msg_Default_Runs_Wrn()
	{
		// Arrange
		var msg = Substitute.ForPartsOf<Msg>();
		var log = Substitute.For<Log>();

		// Act
		log.Msg(msg);

		// Assert
		log.Received().Wrn(Arg.Any<string>(), Arg.Any<object[]>());
	}

	[Fact]
	public void Msg_Trace_Runs_Vrb()
	{
		// Arrange
		var msg = Substitute.For<Msg>();
		msg.Level.Returns(LogLevel.Verbose);
		var log = Substitute.For<Log>();

		// Act
		log.Msg(msg);

		// Assert
		log.Received().Vrb(Arg.Any<string>(), Arg.Any<object[]>());
	}

	[Fact]
	public void Msg_Debug_Runs_Dbg()
	{
		// Arrange
		var msg = Substitute.For<Msg>();
		msg.Level.Returns(LogLevel.Debug);
		var log = Substitute.For<Log>();

		// Act
		log.Msg(msg);

		// Assert
		log.Received().Dbg(Arg.Any<string>(), Arg.Any<object[]>());
	}

	[Fact]
	public void Msg_Information_Runs_Inf()
	{
		// Arrange
		var msg = Substitute.For<Msg>();
		msg.Level.Returns(LogLevel.Information);
		var log = Substitute.For<Log>();

		// Act
		log.Msg(msg);

		// Assert
		log.Received().Inf(Arg.Any<string>(), Arg.Any<object[]>());
	}

	[Fact]
	public void Msg_Warning_Runs_Wrn()
	{
		// Arrange
		var msg = Substitute.For<Msg>();
		msg.Level.Returns(LogLevel.Warning);
		var log = Substitute.For<Log>();

		// Act
		log.Msg(msg);

		// Assert
		log.Received().Wrn(Arg.Any<string>(), Arg.Any<object[]>());
	}

	[Fact]
	public void Msg_Error_Runs_Err()
	{
		// Arrange
		var msg = Substitute.For<Msg>();
		msg.Level.Returns(LogLevel.Error);
		var log = Substitute.For<Log>();

		// Act
		log.Msg(msg);

		// Assert
		log.Received().Err(Arg.Any<string>(), Arg.Any<object[]>());
	}

	[Fact]
	public void Msg_Fatal_Runs_Ftl()
	{
		// Arrange
		var msg = Substitute.For<Msg>();
		msg.Level.Returns(LogLevel.Fatal);
		var log = Substitute.For<Log>();

		// Act
		log.Msg(msg);

		// Assert
		log.Received().Ftl(Arg.Any<string>(), Arg.Any<object[]>());
	}
}
