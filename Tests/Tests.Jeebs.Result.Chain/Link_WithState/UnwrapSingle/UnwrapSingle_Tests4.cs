using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.Link_Tests.WithState
{
	public partial class UnwrapSingle_Tests
	{
		[Fact]
		public void Not_IEnumerable_Or_Same_Type_Input_Returns_IError()
		{
			// Arrange
			var value = F.Rnd.Integer;
			var state = F.Rnd.Integer;
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
