using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.ROk_Tests.WithState
{
	public class Ok_Tests : IOk_Ok
	{
		[Fact]
		public void Returns_Original_Object()
		{
			// Arrange
			var state = F.Rnd.Integer;
			var r = Result.Ok(state);

			// Act
			var next = r.Ok();

			// Assert
			Assert.StrictEqual(r, next);
			Assert.Equal(state, next.State);
		}

		[Fact]
		public void Same_Type_Returns_Original_Object()
		{
			// Arrange
			var state = F.Rnd.Integer;
			var r = Result.Ok(state);

			// Act
			var next = r.Ok<bool>();

			// Assert
			Assert.StrictEqual(r, next);
			Assert.Equal(state, next.State);
		}

		[Fact]
		public void Different_Type_Keeps_Messages()
		{
			// Arrange
			var m0 = new IntMsg(F.Rnd.Integer);
			var m1 = new StringMsg(F.Rnd.String);
			var state = F.Rnd.Integer;
			var r = Result.Ok(state).AddMsg(m0, m1);

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
