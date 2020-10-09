﻿using System;
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
			var state = F.Rnd.Integer;
			var r = Result.Ok(state);
			var value = F.Rnd.Integer;

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
			var state = F.Rnd.Integer;
			var value = F.Rnd.Integer;
			var m0 = new IntMsg(F.Rnd.Integer);
			var m1 = new StringMsg(F.Rnd.String);
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
