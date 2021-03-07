// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Link_Tests.WithState
{
	public partial class UnwrapSingle_Tests
	{
		[Fact]
		public void Not_IEnumerable_Or_Same_Type_Input_Returns_IError()
		{
			// Arrange
			var value = F.Rnd.Int;
			var state = F.Rnd.Int;
			var chain = Chain.CreateV(value, state);

			// Act
			var result = chain.Link().UnwrapSingle<string>();
			var msg = result.Messages.Get<Jm.Link.Unwrap.NotIEnumerableMsg>();

			// Assert
			var e = Assert.IsAssignableFrom<IError<string, int>>(result);
			Assert.Equal(state, e.State);
			Assert.NotEmpty(msg);
		}
	}
}
