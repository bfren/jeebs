using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.R_Tests
{
	public class False_Tests : IR_False
	{
		[Fact]
		public void Returns_Error_Bool()
		{
			// Arrange
			var r = Result.Ok();

			// Act
			var f = r.False();

			// Assert
			Assert.IsAssignableFrom<IError<bool>>(f);
		}

		[Fact]
		public void With_Message_Returns_Error_With_Msg()
		{
			// Arrange
			var r = Result.Ok();
			var msg = new MsgTest();

			// Act
			var f = r.False(msg);

			// Assert
			Assert.True(f.Messages.Contains<MsgTest>());
		}

		public class MsgTest : IMsg { }
	}
}
