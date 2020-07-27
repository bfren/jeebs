using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result2
{
	public class Messages_Tests
	{
		[Fact]
		public void Messages_Adds_Messages()
		{
			// Arrange
			var r = R.Ok();
			var with = r.With();
			var m0 = new Jm.WithIntMsg(18);
			var m1 = new Jm.WithStringMsg("July");

			// Act
			with.Messages(m0, m1);

			// Assert
			Assert.Equal(2, r.Messages.Count);
			Assert.True(r.Messages.Contains<Jm.WithIntMsg>());
			Assert.True(r.Messages.Contains<Jm.WithStringMsg>());
		}
	}
}
