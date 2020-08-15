using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.MsgList_Tests
{
	public class Contains_Tests
	{
		[Fact]
		public void Returns_False_When_No_Message()
		{
			// Arrange
			var l0 = new MsgList();
			var l1 = new MsgList();

			// Act
			l1.Add<TestMsg>();

			// Assert
			Assert.False(l0.Contains<TestMsg>());
			Assert.False(l1.Contains<StringMsg>());
		}

		[Fact]
		public void Matches_Message_Added_As_Object()
		{
			// Arrange
			var l = new MsgList();
			var m = new TestMsg();

			// Act
			l.Add(m);

			// Assert
			Assert.True(l.Contains<TestMsg>());
		}

		[Fact]
		public void Matches_Message_Added_As_Type()
		{
			// Arrange
			var l = new MsgList();

			// Act
			l.Add<TestMsg>();

			// Assert
			Assert.True(l.Contains<TestMsg>());
		}

		private class TestMsg : IMsg { }

		public class StringMsg : Jm.WithValueMsg<string>
		{
			public StringMsg(string value) : base(value) { }
		}
	}
}
