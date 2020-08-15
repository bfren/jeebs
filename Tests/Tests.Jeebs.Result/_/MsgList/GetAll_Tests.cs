using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.MsgList_Tests
{
	public class GetAll_Tests
	{
		[Fact]
		public void Returns_EmptyList_When_No_Messages()
		{
			// Arrange
			var l = new MsgList();

			// Act

			// Assert
			Assert.Empty(l.GetAll());
		}

		[Fact]
		public void Returns_List_Of_Message_Strings()
		{
			// Arrange
			var l = new MsgList();

			var m0 = new StringMsg("zero");
			var m1 = new StringMsg("one");
			var ms = new List<string>(new[] { "zero", "one" });

			// Act
			l.AddRange(m0, m1);

			// Assert
			Assert.Equal(ms, l.GetAll());
		}

		public class StringMsg : Jm.WithValueMsg<string>
		{
			public StringMsg(string value) : base(value) { }
		}
	}
}
