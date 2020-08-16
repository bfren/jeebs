using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.ROk_Tests.WithState
{
	public class OkV_Tests : IOk_OkV
	{
		[Fact]
		public void Returns_Object_With_Value()
		{
			// Arrange
			const int state = 18;
			var r = Result.Ok(state);
			const int value = 7;

			// Act
			var next = r.OkV(value);

			// Assert
			Assert.IsAssignableFrom<IOkV<int>>(next);
			Assert.IsAssignableFrom<IOkV<int, int>>(next);
			Assert.Equal(state, next.State);
			Assert.Equal(value, next.Value);
		}

		[Fact]
		public void Keeps_Messages()
		{
			// Arrange
			const int state = 18;
			const int value = 7;
			var m0 = new IntMsg(7);
			var m1 = new StringMsg("July");
			var r = Result.Ok(state).AddMsg(m0, m1);

			// Act
			var next = r.OkV(value);

			// Assert
			Assert.Equal(2, next.Messages.Count);
			Assert.True(next.Messages.Contains<IntMsg>());
			Assert.True(next.Messages.Contains<StringMsg>());
		}

		public class IntMsg : Jm.WithValueMsg<int>
		{
			public IntMsg(int value) : base(value) { }
		}

		public class StringMsg : Jm.WithValueMsg<string>
		{
			public StringMsg(string value) : base(value) { }
		}
	}
}
