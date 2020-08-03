using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs
{
	public class R_False_Tests
	{
		[Fact]
		public void False_Returns_Error_Bool()
		{
			// Arrange
			var r = R.Ok();

			// Act
			var f = r.False();

			// Assert
			Assert.IsAssignableFrom<IError<bool>>(f);
		}

		[Fact]
		public void False_With_Message_Returns_Error_With_Msg()
		{
			// Arrange
			var r = R.Ok();
			var msg = new MsgTest();

			// Act
			var f = r.False(msg);

			// Assert
			Assert.True(f.Messages.Contains<MsgTest>());
		}

		public class MsgTest : IMsg { }
	}
}
