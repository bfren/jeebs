using Xunit;

namespace Jeebs.Result_Tests
{
	public class OkV_Tests
	{
		[Fact]
		public void Returns_OkV()
		{
			// Arrange
			const int value = 18;

			// Act
			var r = Result.OkV(value);

			// Assert
			Assert.IsAssignableFrom<IOk>(r);
			Assert.IsAssignableFrom<IOkV<int>>(r);
			Assert.Equal(value, r.Value);
		}

		[Fact]
		public void With_State_Returns_OkV_Sets_State()
		{
			// Arrange
			const int value = 18;
			const int state = 7;

			// Act
			var r = Result.OkV(value, state);

			// Assert
			Assert.IsAssignableFrom<IOk>(r);
			Assert.IsAssignableFrom<IOkV<int>>(r);
			Assert.IsAssignableFrom<IOkV<int, int>>(r);
			Assert.Equal(value, r.Value);
			Assert.Equal(state, r.State);
		}
	}
}
