using System;
using System.Collections.Generic;
using Jeebs;
using Xunit;

namespace Jeebs.ROk_Tests
{
	public class True_Tests : IOk_True
	{
		[Fact]
		public void Returns_IOk_Bool()
		{
			// Arrange
			var r = Result.Ok();

			// Act
			var f = r.True();

			// Assert
			Assert.IsAssignableFrom<IOk<bool>>(f);
		}

		[Fact]
		public void With_Message_Returns_IOk_With_Msg()
		{
			// Arrange
			var r = Result.Ok();
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
