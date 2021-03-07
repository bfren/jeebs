// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Link_Tests.WithState
{
	public partial class UnwrapSingle_Tests
	{
		[Fact]
		public void IEnumerable_Input_Incorrect_Subtype_Returns_IError()
		{
			// Arrange
			var list = new[] { 1, 2 };
			var state = F.Rnd.Int;
			var chain = Chain.CreateV(list, state);

			// Act
			var result = chain.Link().UnwrapSingle<string>();
			var msg = result.Messages.Get<Jm.Link.Unwrap.IncorrectTypeMsg>();

			// Assert
			var e = Assert.IsAssignableFrom<IError<string, int>>(result);
			Assert.Equal(state, e.State);
			Assert.NotEmpty(msg);
		}
	}
}
