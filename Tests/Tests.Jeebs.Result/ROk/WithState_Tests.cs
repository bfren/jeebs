using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.ROk_Tests
{
	public class WithState_Tests : IOk_WithState
	{
		[Fact]
		public void Returns_Ok_With_State()
		{
			// Arrange
			var state = F.Rnd.Integer;
			var r0 = Result.Ok();
			var r1 = Result.Ok<string>();

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
		public void Returns_Ok_With_State_And_Keeps_Value()
		{
			// Arrange
			var value = F.Rnd.Integer;
			var state = F.Rnd.Integer;
			var result = Result.OkV(value);

			// Act
			var next = result.WithState(state);

			// Assert
			Assert.Equal(value, next.Value);
			Assert.Equal(state, next.State);
		}

		[Fact]
		public void Returns_Ok_With_State_And_Keeps_Messages()
		{
			// Arrange
			var state = F.Rnd.Integer;
			var r = Result.Ok();
			r.AddMsg(new StringMsg(F.Rnd.String));

			// Act
			var next = r.WithState(state);

			// Assert
			Assert.True(next.Messages.Contains<StringMsg>());
		}

		public class StringMsg : Jm.WithValueMsg<string>
		{
			public StringMsg(string value) : base(value) { }
		}
	}
}
