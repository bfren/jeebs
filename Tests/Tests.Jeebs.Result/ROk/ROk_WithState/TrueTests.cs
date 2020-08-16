using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.ROk_Tests.WithState
{
	public class True_Tests : IOk_True
	{
		[Fact]
		public void Returns_IOk_Bool()
		{
			// Arrange
			const int state = 7;
			var r = Result.Ok(state);

			// Act
			var f = r.True();

			// Assert
			Assert.IsAssignableFrom<IOk<bool, int>>(f);
		}

		[Fact]
		public void With_Message_Returns_IOk_With_Msg()
		{
			// Arrange
			const int state = 7;
			var r = Result.Ok(state);
			var msg = new MsgTest();

			// Act
			var f = r.True(msg);

			// Assert
			Assert.IsAssignableFrom<IOk<bool, int>>(f);
			Assert.True(f.Messages.Contains<MsgTest>());
		}

		public class MsgTest : IMsg { }
	}
}
