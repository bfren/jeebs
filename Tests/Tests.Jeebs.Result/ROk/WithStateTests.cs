using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result
{
	public class ROk_WithState_Tests
	{
		[Fact]
		public void WithState_Returns_Ok_With_State()
		{
			// Arrange
			const int state = 18;
			var r = R.Ok();

			// Act
			var next = r.WithState(state);

			// Assert
			Assert.IsAssignableFrom<IOk<bool, int>>(next);
			Assert.Equal(state, next.State);
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
