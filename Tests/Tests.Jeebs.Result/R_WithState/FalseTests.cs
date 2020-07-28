using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result
{
	public class R_WithState_False_Tests
	{
		[Fact]
		public void False_Returns_Error_Bool()
		{
			// Arrange
			const int state = 18;
			var r = R.Ok(state);

			// Act
			var f = r.False();

			// Assert
			Assert.IsAssignableFrom<IError<bool, int>>(f);
		}

		[Fact]
		public void False_With_Message_Returns_Error_With_Msg()
		{
			// Arrange
			const int state = 18;
			var r = R.Ok(state);
			var msg = new MsgTest();

			// Act
			var f = r.False(msg);

			// Assert
			Assert.IsAssignableFrom<IError<bool, int>>(f);
			Assert.True(f.Messages.Contains<MsgTest>());
		}

		public class MsgTest : IMsg { }
	}
}
