using Xunit;

namespace Jeebs.Link_Tests.WithState
{
	public partial class UnwrapSingle_Tests
	{
		[Fact]
		public void IEnumberable_Input_Multiple_Items_Returns_IError()
		{
			// Arrange
			var list = new[] { 1, 2 };
			var state = F.Rnd.Int;
			var chain = Chain.CreateV(list, state);

			// Act
			var result = chain.Link().UnwrapSingle<int>();
			var msg = result.Messages.Get<Jm.Link.Unwrap.MoreThanOneItemMsg>();

			// Assert
			var e = Assert.IsAssignableFrom<IError<int, int>>(result);
			Assert.Equal(state, e.State);
			Assert.NotEmpty(msg);
		}

		[Fact]
		public void List_Input_Multiple_Items_Returns_IError()
		{
			// Arrange
			var list = new[] { 1, 2 };
			var state = F.Rnd.Int;
			var chain = Chain.CreateV(list, state);

			// Act
			var result = chain.Link().UnwrapSingle<int>();
			var msg = result.Messages.Get<Jm.Link.Unwrap.MoreThanOneItemMsg>();

			// Assert
			var e = Assert.IsAssignableFrom<IError<int, int>>(result);
			Assert.Equal(state, e.State);
			Assert.NotEmpty(msg);
		}

		[Fact]
		public void Custom_Input_Multiple_Items_Returns_IError()
		{
			// Arrange
			var list = new CustomList(1, 2);
			var state = F.Rnd.Int;
			var chain = Chain.CreateV(list, state);

			// Act
			var result = chain.Link().UnwrapSingle<int>();
			var msg = result.Messages.Get<Jm.Link.Unwrap.MoreThanOneItemMsg>();

			// Assert
			var e = Assert.IsAssignableFrom<IError<int, int>>(result);
			Assert.Equal(state, e.State);
			Assert.NotEmpty(msg);
		}
	}
}
