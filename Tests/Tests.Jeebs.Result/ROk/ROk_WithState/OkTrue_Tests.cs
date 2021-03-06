using Xunit;

namespace Jeebs.ROk_Tests.WithState
{
	public class OkTrue_Tests : IOk_Boolean
	{
		[Fact]
		public void Returns_IOk_Bool()
		{
			// Arrange
			var state = F.Rnd.Int;
			var r = Result.Ok(state);

			// Act
			var f = r.OkTrue();

			// Assert
			var ok = Assert.IsAssignableFrom<IOkV<bool, int>>(f);
			Assert.True(ok.Value);
		}

		[Fact]
		public void With_Message_Returns_IOk_With_Msg()
		{
			// Arrange
			var state = F.Rnd.Int;
			var r = Result.Ok(state);
			var msg = new MsgTest();

			// Act
			var f = r.OkTrue(msg);

			// Assert
			var ok = Assert.IsAssignableFrom<IOkV<bool, int>>(f);
			Assert.True(ok.Value);
			Assert.True(f.Messages.Contains<MsgTest>());
		}

		public class MsgTest : IMsg { }
	}
}
