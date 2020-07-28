using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result2
{
	public class ROk_Ok_Tests
	{
		[Fact]
		public void Ok_Returns_Original_Object()
		{
			// Arrange
			var r = R.Ok();

			// Act
			var next = r.Ok();

			// Assert
			Assert.StrictEqual(r, next);
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
			var m0 = new Jm.WithIntMsg(18);
			var m1 = new Jm.WithStringMsg("July");
			var r = R.Ok().AddMsg().Messages(m0, m1);

			// Act
			var next = r.Ok<int>();

			// Assert
			Assert.Equal(2, next.Messages.Count);
			Assert.True(next.Messages.Contains<Jm.WithIntMsg>());
			Assert.True(next.Messages.Contains<Jm.WithStringMsg>());
		}
	}
}
