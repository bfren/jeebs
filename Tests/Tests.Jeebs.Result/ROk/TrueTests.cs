using System;
using System.Collections.Generic;
using Jeebs;
using Xunit;

namespace Jeebs
{
	public class ROk_True_Tests
	{
		[Fact]
		public void True_Returns_IOk_Bool()
		{
			// Arrange
			var r = R.Ok();

			// Act
			var f = r.True();

			// Assert
			Assert.IsAssignableFrom<IOk<bool>>(f);
		}

		[Fact]
		public void True_With_Message_Returns_IOk_With_Msg()
		{
			// Arrange
			var r = R.Ok();
			var msg = new MsgTest();

			// Act
			var f = r.True(msg);

			// Assert
			Assert.IsAssignableFrom<IOk<bool>>(f);
			Assert.True(f.Messages.Contains<MsgTest>());
		}

		public class MsgTest : IMsg { }
	}
}
