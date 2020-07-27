using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result2
{
	public class Message_Tests
	{
		[Fact]
		public void Message_By_Type_Adds_Message()
		{
			// Arrange
			var r = R.Ok();
			var with = r.With();

			// Act
			with.Message<MsgTest>();

			// Assert
			Assert.True(r.Messages.Contains<MsgTest>());
		}

		[Fact]
		public void Message_By_Object_Adds_Message()
		{
			// Arrange
			var r = R.Ok();
			var with = r.With();
			var msg = new MsgTest();

			// Act
			with.Message(msg);

			// Assert
			Assert.True(r.Messages.Contains<MsgTest>());
		}

		public class MsgTest : IMsg { }
	}
}
