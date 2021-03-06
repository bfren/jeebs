using Xunit;

namespace Jeebs.Link_Tests.WithState
{
	public partial class Wrap_Tests : ILink_Wrap_WithState
	{
		[Fact]
		public void Value_Input_When_IOk_Wraps_Value()
		{
			// Arrange
			var value = F.Rnd.Int;
			var state = F.Rnd.Int;
			var r = Chain.Create(state);

			// Act
			var next = r.Link().Wrap(value);

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int, int>>(next);
			Assert.Equal(value, okV.Value);
			Assert.Equal(state, okV.State);
		}

		[Fact]
		public void Value_Input_When_IError_Returns_IError()
		{
			// Arrange
			var value = F.Rnd.Int;
			var state = F.Rnd.Int;
			var r = Chain.Create(state).Error();

			// Act
			var next = r.Link().Wrap(value);

			// Assert
			var e = Assert.IsAssignableFrom<IError<int, int>>(next);
			Assert.Equal(state, e.State);
		}
	}
}
