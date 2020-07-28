using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result
{
	public class ROk_WithState_OkV_Tests
	{
		[Fact]
		public void OkV_Returns_Object_With_Value_And_State()
		{
			// Arrange
			var state = 18;
			var r = R.Ok(state);
			var value = 7;

			// Act
			var next = r.OkV(value);

			// Assert
			Assert.IsAssignableFrom<IOkV<int>>(next);
			Assert.IsAssignableFrom<IOkV<int, int>>(next);
			Assert.Equal(state, next.State);
			Assert.Equal(value, next.Value);
		}

		[Fact]
		public void OkV_Keeps_Messages()
		{
			// Arrange
			var state = 18;
			var value = 7;
			var m0 = new Jm.WithIntMsg(7);
			var m1 = new Jm.WithStringMsg("July");
			var r = R.Ok(state).AddMsg(m0, m1);

			// Act
			var next = r.OkV(value);

			// Assert
			Assert.Equal(2, next.Messages.Count);
			Assert.True(next.Messages.Contains<Jm.WithIntMsg>());
			Assert.True(next.Messages.Contains<Jm.WithStringMsg>());
		}
	}
}
