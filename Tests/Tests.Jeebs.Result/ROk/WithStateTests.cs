using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs
{
	public class ROk_WithState_Tests
	{
		[Fact]
		public void WithState_Returns_Ok_With_State()
		{
			// Arrange
			const int state = 18;
			var r0 = R.Ok();
			var r1 = R.Ok<string>();

			// Act
			var n0 = r0.WithState(state);
			var n1 = r1.WithState(state);

			// Assert
			Assert.IsAssignableFrom<IOk<bool, int>>(n0);
			Assert.Equal(state, n0.State);
			Assert.IsAssignableFrom<IOk<string, int>>(n1);
			Assert.Equal(state, n1.State);
		}

		[Fact]
		public void WithState_Returns_Ok_With_State_And_Keeps_Messages()
		{
			// Arrange
			const int state = 18;
			var r = R.Ok();
			r.AddMsg("Test message.");

			// Act
			var next = r.WithState(state);

			// Assert
			Assert.True(next.Messages.Contains<Jm.WithStringMsg>());
		}
	}
}
