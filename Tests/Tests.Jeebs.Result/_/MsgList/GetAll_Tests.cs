// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
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

			var m0 = new StringMsg(F.Rnd.Str);
			var m1 = new StringMsg(F.Rnd.Str);
			var expected = new List<string>(new[]
			{
				string.Format(Jm.WithValueMsg.Format, nameof(StringMsg), m0.Value),
				string.Format(Jm.WithValueMsg.Format, nameof(StringMsg), m1.Value)
			});

			// Act
			l.AddRange(m0, m1);

			// Assert
			Assert.Equal(expected, l.GetAll());
		}

		public class StringMsg : Jm.WithValueMsg<string>
		{
			public StringMsg(string value) : base(value) { }
		}
	}
}
