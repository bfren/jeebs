using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs
{
	public class ROkV_WithState_Ok_Tests
	{
		[Fact]
		public void Ok_Returns_Original_Object()
		{
			// Arrange
			const int state = 18;
			var r = R.Ok(state);

			// Act
			var next = r.Ok();

			// Assert
			Assert.StrictEqual(r, next);
			Assert.Equal(state, next.State);
		}

		[Fact]
		public void Ok_Same_Type_Returns_Original_Object()
		{
			// Arrange
			const int state = 18;
			var r = R.Ok(state);

			// Act
			var next = r.Ok<bool>();

			// Assert
			Assert.StrictEqual(r, next);
			Assert.Equal(state, next.State);
		}

		[Fact]
		public void Ok_Different_Type_Keeps_Messages()
		{
			// Arrange
			var m0 = new IntMsg(18);
			var m1 = new StringMsg("July");
			const int state = 18;
			var r = R.Ok(state).AddMsg(m0, m1);

			// Act
			var next = r.Ok<int>();

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
