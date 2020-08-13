﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs
{
	public class ROk_Ok_Tests
	{
		[Fact]
		public void Ok_Returns_Original_Object()
		{
			// Arrange
			var r0 = R.Ok();
			var r1 = R.Ok<int>();

			// Act
			var n0 = r0.Ok();
			var n1 = r1.Ok();

			// Assert
			Assert.StrictEqual(r0, n0);
			Assert.StrictEqual(r1, n1);
		}

		[Fact]
		public void Ok_Same_Type_Returns_Original_Object()
		{
			// Arrange
			var r = R.Ok();

			// Act
			var next = r.Ok<bool>();

			// Assert
			Assert.StrictEqual(r, next);
		}

		[Fact]
		public void Ok_Different_Type_Keeps_Messages()
		{
			// Arrange
			var m0 = new IntMsg(18);
			var m1 = new StringMsg("July");
			var r = R.Ok().AddMsg(m0, m1);

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
