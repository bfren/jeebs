using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.MsgExtensions_Tests
{
	public class Prepare_Tests
	{
		[Fact]
		public void Msg_ToString_Is_Type_Returns_Msg_Type_Empty_Args()
		{
			// Arrange
			var msg = new MsgTest();

			// Act
			var (text, args) = msg.Prepare();

			// Assert
			Assert.Equal(text, msg.ToString());
			Assert.Empty(args);
		}

		[Fact]
		public void Msg_ToString_Is_Not_Type_Adds_Type_To_Return()
		{
			// Arrange
			const string error = "Error message";
			var msg = new MsgWithValueTest(error);

			// Act
			var (text, args) = msg.Prepare();

			// Assert
			Assert.Equal($"{{MsgType}} - {error}", text);
			var arg = Assert.Single(args);
			Assert.Equal(typeof(MsgWithValueTest).ToString(), arg);
		}

		[Fact]
		public void LoggableMsg_Adds_Type_To_Return()
		{
			// Arrange
			const string format = "{0} {1}";
			var values = new object[] { 2, 3 };
			var msg = Substitute.For<ILoggableMsg>();
			msg.Format.Returns(format);
			msg.ParamArray.Returns(values);

			// Act
			var (text, args) = msg.Prepare();

			// Assert
			Assert.Equal($"{{MsgType}} - {format}", text);
			Assert.Equal(3, args.Length);
			Assert.Equal(msg.GetType().ToString(), args[0]);
			Assert.Equal(2, args[1]);
			Assert.Equal(3, args[2]);
		}

		public class MsgTest : IMsg { }

		public class MsgWithValueTest : IMsg
		{
			private readonly string value;

			public MsgWithValueTest(string value)
				=> this.value = value;

			public override string ToString()
				=> value;
		}
	}
}
