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
		public void Incorrect_Subtype_Returns_IError()
		{
			// Arrange
			var list = new[] { 1, 2 };
			const int state = 7;
			var chain = Chain.CreateV(list, state);

			// Act
			var result = chain.Link().Single<string>();
			var msg = result.Messages.Get<Jm.IncorrectSingleTypeMsg>();

			// Assert
			var e = Assert.IsAssignableFrom<IError<string, int>>(result);
			Assert.Equal(state, e.State);
			Assert.NotEmpty(msg);
		}
	}
}
