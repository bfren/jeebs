using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.UnwrapTests.WithState
{
	public partial class UnwrapTests
	{
		[Fact]
		public void IEnumerable_Input_Incorrect_Subtype_Returns_IError()
		{
			// Arrange
			var list = new[] { 1, 2 };
			const int state = 7;
			var chain = Chain.CreateV(list, state);

			// Act
			var result = chain.Link().Unwrap<string>();
			var msg = result.Messages.Get<Jm.Link.Single.IncorrectTypeMsg>();

			// Assert
			var e = Assert.IsAssignableFrom<IError<string, int>>(result);
			Assert.Equal(state, e.State);
			Assert.NotEmpty(msg);
		}
	}
}
