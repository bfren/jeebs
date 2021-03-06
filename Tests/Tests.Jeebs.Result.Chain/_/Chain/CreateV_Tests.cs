using Xunit;

namespace Jeebs.Chain_Tests
{
	public class CreateV_Tests
	{
		[Fact]
		public void Returns_OkV()
		{
			// Arrange
			var value = F.Rnd.Int;

			// Act
			var r = Chain.CreateV(value);

			// Assert
			Assert.IsAssignableFrom<IOkV<int>>(r);
			Assert.Equal(value, r.Value);
		}

		[Fact]
		public void With_State_Returns_OkV_With_State()
		{
			// Arrange
			var value = F.Rnd.Int;
			var state = F.Rnd.Int;

			// Act
			var r = Chain.CreateV(value, state);

			// Assert
			Assert.IsAssignableFrom<IOkV<int, int>>(r);
			Assert.Equal(value, r.Value);
			Assert.Equal(state, r.State);
		}
	}
}
