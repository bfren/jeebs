using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.RExtensions_Tests
{
	public class AddMsg_Tests
	{
		[Fact]
		public void Of_Type_Adds_Message()
		{
			// Arrange
			var r = Result.Ok();
			var with = r.AddMsg();

			// Act
			with.OfType<MsgTest>();

			// Assert
			Assert.True(r.Messages.Contains<MsgTest>());
		}

		[Fact]
		public void By_String_Adds_Message()
		{
			// Arrange
			var r = Result.Ok();
			var msg = new StringMsg(F.Rand.String);

			// Act
			r.AddMsg(msg);

			// Assert
			Assert.True(r.Messages.Contains<StringMsg>());
		}

		[Fact]
		public void By_Object_Adds_Message()
		{
			// Arrange
			var r = Result.Ok();
			var msg = new MsgTest();

			// Act
			r.AddMsg(msg);

			// Assert
			Assert.True(r.Messages.Contains<MsgTest>());
		}

		[Fact]
		public void By_Params_Adds_Messages()
		{
			// Arrange
			var r = Result.Ok();
			var m0 = new IntMsg(18);
			var m1 = new StringMsg(F.Rand.String);

			// Act
			r.AddMsg(m0, m1);

			// Assert
			Assert.Equal(2, r.Messages.Count);
			Assert.True(r.Messages.Contains<IntMsg>());
			Assert.True(r.Messages.Contains<StringMsg>());
		}

		public class MsgTest : IMsg { }

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
