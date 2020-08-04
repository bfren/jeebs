using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.SingleTests.WithState
{
	public partial class SingleTests
	{
		[Fact]
		public void Not_IEnumerable_Input_Returns_IError()
		{
			// Arrange
			const int value = 18;
			const int state = 7;
			var chain = Chain.CreateV(value, state);

			// Act
			var result = chain.Link().Single<int>();
			var msg = result.Messages.Get<Jm.NotIEnumerableMsg>();

			// Assert
			var e = Assert.IsAssignableFrom<IError<int, int>>(result);
			Assert.Equal(state, e.State);
			Assert.NotEmpty(msg);
		}
	}
}
