using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result2
{
	public class ROk_OkV_Tests
	{
		[Fact]
		public void OkV_Returns_Object_With_Value()
		{
			// Arrange
			var r = R.Ok();
			var value = 18;

			// Act
			var next = r.OkV(value);

			// Assert
			Assert.IsAssignableFrom<IOkV<int>>(next);
			Assert.Equal(value, next.Value);
		}

		[Fact]
		public void OkV_Keeps_Messages()
		{
			// Arrange
			var value = 18;
			var m0 = new Jm.WithIntMsg(7);
			var m1 = new Jm.WithStringMsg("July");
			var r = R.Ok().With().Messages(m0, m1);

			// Act
			var next = r.OkV(value);

			// Assert
			Assert.Equal(2, next.Messages.Count);
			Assert.True(next.Messages.Contains<Jm.WithIntMsg>());
			Assert.True(next.Messages.Contains<Jm.WithStringMsg>());
		}
	}
}
