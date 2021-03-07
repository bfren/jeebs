// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.ROk_Tests.WithState
{
	public class OkFalse_Tests : IOk_Boolean
	{
		[Fact]
		public void Returns_IOk_Bool()
		{
			// Arrange
			var state = F.Rnd.Int;
			var r = Result.Ok(state);

			// Act
			var f = r.OkFalse();

			// Assert
			var ok = Assert.IsAssignableFrom<IOkV<bool, int>>(f);
			Assert.False(ok.Value);
		}

		[Fact]
		public void With_Message_Returns_IOk_With_Msg()
		{
			// Arrange
			var state = F.Rnd.Int;
			var r = Result.Ok(state);
			var msg = new MsgTest();

			// Act
			var f = r.OkFalse(msg);

			// Assert
			var ok = Assert.IsAssignableFrom<IOkV<bool, int>>(f);
			Assert.False(ok.Value);
			Assert.True(f.Messages.Contains<MsgTest>());
		}

		public class MsgTest : IMsg { }
	}
}
