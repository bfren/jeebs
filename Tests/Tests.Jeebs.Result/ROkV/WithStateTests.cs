using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs
{
	public class ROkV_WithState_Tests
	{
		[Fact]
		public void WithState_Returns_OkV_With_State()
		{
			// Arrange
			const int value = 18;
			const int state = 7;
			var r = R.OkV(value);

			// Act
			var next = r.WithState(state);

			// Assert
			Assert.IsAssignableFrom<IOk<int, int>>(next);
			Assert.Equal(value, next.Value);
			Assert.Equal(state, next.State);
		}

		[Fact]
		public void WithState_Returns_OkV_With_State_And_Keeps_Messages()
		{
			// Arrange
			const int value = 18;
			const int state = 7;
			var r = R.OkV(value);
			r.AddMsg(new StringMsg("Test message."));

			// Act
			var next = r.WithState(state);

			// Assert
			Assert.True(next.Messages.Contains<StringMsg>());
			Assert.Equal(value, next.Value);
		}

		public class StringMsg : Jm.WithValueMsg<string>
		{
			public StringMsg(string value) : base(value) { }
		}
	}
}
