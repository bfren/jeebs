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
			var error = F.Rnd.Str;
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
			var n0 = F.Rnd.Int;
			var n1 = F.Rnd.Int;
			var values = new object[] { n0, n1 };
			var msg = Substitute.For<ILoggableMsg>();
			msg.Format.Returns(format);
			msg.ParamArray.Returns(values);

			// Act
			var (text, args) = msg.Prepare();

			// Assert
			Assert.Equal($"{{MsgType}} - {format}", text);
			Assert.Equal(3, args.Length);
			Assert.Equal(msg.GetType().ToString(), args[0]);
			Assert.Equal(n0, args[1]);
			Assert.Equal(n1, args[2]);
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
