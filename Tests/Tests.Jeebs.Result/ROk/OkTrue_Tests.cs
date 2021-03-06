using Xunit;

namespace Jeebs.ROk_Tests
{
	public class OkTrue_Tests : IOk_Boolean
	{
		[Fact]
		public void Returns_IOk_Bool()
		{
			// Arrange
			var r = Result.Ok();

			// Act
			var f = r.OkTrue();

			// Assert
			var ok = Assert.IsAssignableFrom<IOkV<bool>>(f);
			Assert.True(ok.Value);
		}

		[Fact]
		public void With_Message_Returns_IOk_With_Msg()
		{
			// Arrange
			var r = Result.Ok();
			var msg = new MsgTest();

			// Act
			var f = r.OkTrue(msg);

			// Assert
			var ok = Assert.IsAssignableFrom<IOkV<bool>>(f);
			Assert.True(ok.Value);
			Assert.True(f.Messages.Contains<MsgTest>());
		}

		public class MsgTest : IMsg { }
	}
}
