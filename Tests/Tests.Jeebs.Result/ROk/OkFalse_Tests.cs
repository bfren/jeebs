// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.ROk_Tests
{
	public class OkFalse_Tests : IOk_Boolean
	{
		[Fact]
		public void Returns_IOk_Bool()
		{
			// Arrange
			var r = Result.Ok();

			// Act
			var f = r.OkFalse();

			// Assert
			var ok = Assert.IsAssignableFrom<IOkV<bool>>(f);
			Assert.False(ok.Value);
		}

		[Fact]
		public void With_Message_Returns_IOk_With_Msg()
		{
			// Arrange
			var r = Result.Ok();
			var msg = new MsgTest();

			// Act
			var f = r.OkFalse(msg);

			// Assert
			var ok = Assert.IsAssignableFrom<IOkV<bool>>(f);
			Assert.False(ok.Value);
			Assert.True(f.Messages.Contains<MsgTest>());
		}

		public class MsgTest : IMsg { }
	}
}
