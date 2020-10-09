using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.MsgList_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void Returns_TypeName_When_Empty()
		{
			// Arrange
			var l = new MsgList();
			var t = typeof(MsgList).FullName;

			// Act

			// Assert
			Assert.Equal(t, l.ToString());
		}

		[Fact]
		public void Returns_Message_Strings_On_NewLines()
		{
			// Arrange
			var l = new MsgList();

			var m0 = new StringMsg(F.Rand.String);
			var m1 = new StringMsg(F.Rand.String);

			// Act
			l.AddRange(m0, m1);

			// Assert
			Assert.Equal($"{m0}\n{m1}", l.ToString());
		}

		public class StringMsg : Jm.WithValueMsg<string>
		{
			public StringMsg(string value) : base(value) { }
		}
	}
}
