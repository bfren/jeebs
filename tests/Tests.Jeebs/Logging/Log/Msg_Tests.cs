// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Messages;
using MaybeF;

namespace Jeebs.Logging.Log_Tests;

public class Msg_Tests
{
	[Fact]
	public void Reason_Calls_Inf_With_Type()
	{
		// Arrange
		var log = Substitute.ForPartsOf<Log>();
		var reason = new TestIMsg();

		// Act
		log.Msg(reason);

		// Assert
		log.Received().Inf(typeof(TestIMsg).ToString(), Array.Empty<object>());
	}

	[Fact]
	public void Verbose_Msg_Calls_Vrb()
	{
		// Arrange
		var log = Substitute.ForPartsOf<Log>();
		var msg = Substitute.ForPartsOf<Msg>();
		msg.Level.Returns(LogLevel.Verbose);

		// Act
		log.Msg(msg);

		// Assert
		log.Received().Vrb(Arg.Any<string>(), Arg.Any<object[]>());
	}

	[Fact]
	public void Debug_Msg_Calls_Dbg()
	{
		// Arrange
		var log = Substitute.ForPartsOf<Log>();
		var msg = Substitute.ForPartsOf<Msg>();
		msg.Level.Returns(LogLevel.Debug);

		// Act
		log.Msg(msg);

		// Assert
		log.Received().Dbg(Arg.Any<string>(), Arg.Any<object[]>());
	}

	[Fact]
	public void Information_Msg_Calls_Inf()
	{
		// Arrange
		var log = Substitute.ForPartsOf<Log>();
		var msg = Substitute.ForPartsOf<Msg>();
		msg.Level.Returns(LogLevel.Information);

		// Act
		log.Msg(msg);

		// Assert
		log.Received().Inf(Arg.Any<string>(), Arg.Any<object[]>());
	}

	[Fact]
	public void Warning_Msg_Calls_Wrn()
	{
		// Arrange
		var log = Substitute.ForPartsOf<Log>();
		var msg = Substitute.ForPartsOf<Msg>();
		msg.Level.Returns(LogLevel.Warning);

		// Act
		log.Msg(msg);

		// Assert
		log.Received().Wrn(Arg.Any<string>(), Arg.Any<object[]>());
	}

	[Fact]
	public void Error_Msg_Calls_Err()
	{
		// Arrange
		var log = Substitute.ForPartsOf<Log>();
		var msg = Substitute.ForPartsOf<Msg>();
		msg.Level.Returns(LogLevel.Error);

		// Act
		log.Msg(msg);

		// Assert
		log.Received().Err(Arg.Any<string>(), Arg.Any<object[]>());
	}

	[Fact]
	public void Fatal_Msg_Calls_Ftl()
	{
		// Arrange
		var log = Substitute.ForPartsOf<Log>();
		var msg = Substitute.ForPartsOf<Msg>();
		msg.Level.Returns(LogLevel.Fatal);

		// Act
		log.Msg(msg);

		// Assert
		log.Received().Ftl(Arg.Any<string>(), Arg.Any<object[]>());
	}

	[Fact]
	public void Msg_Passes_Text_With_Type()
	{
		// Arrange
		var log = Substitute.ForPartsOf<Log>();
		var format = Rnd.Str;
		var msg = new TestMsg(format);

		// Act
		log.Msg(msg);

		// Assert
		log.Received().Vrb($"{{MsgType}} {format}", Arg.Any<object[]>());
	}

	[Fact]
	public void Msg_Passes_Args_With_Type()
	{
		// Arrange
		var log = Substitute.ForPartsOf<Log>();
		var a0 = Rnd.Str;
		var a1 = Rnd.Int;
		var args = new object[] { a0, a1 };
		var msg = new TestMsg(args);

		// Act
		log.Msg(msg);

		// Assert
		log.Received().Ftl(Arg.Any<string>(), Arg.Is<object[]>(x =>

			x[0].ToString() == msg.GetType().ToString() &&
			x[1].ToString() == a0 &&
			x[2].ToString() == a1.ToString()
		));
	}

	[Fact]
	public void Calls_For_Each_Msg()
	{
		// Arrange
		var log = Substitute.ForPartsOf<Log>();
		var msg = Substitute.ForPartsOf<Msg>();
		msg.Level.Returns(LogLevel.Verbose);

		// Act
		log.Msgs(new[] { msg, msg, msg, msg, msg });

		// Assert
		log.Received(5).Vrb(Arg.Any<string>(), Arg.Any<object[]>());
	}

	public sealed record class TestIMsg : IMsg;

	public sealed record class TestMsg : Msg
	{
		public TestMsg(string format) =>
			(Level, Format) = (LogLevel.Verbose, format);

		public TestMsg(object[] args) =>
			(Level, Args) = (LogLevel.Fatal, args);
	}
}
