// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.MsgList_Tests
{
	public class Get_Tests
	{
		[Fact]
		public void Returns_EmptyList_When_No_Matching_Messages()
		{
			// Arrange
			var l = new MsgList();

			// Act
			l.Add<TestMsg>();

			// Assert
			Assert.Empty(l.Get<StringMsg>());
		}

		[Fact]
		public void Returns_Only_Matching_Messages()
		{
			// Arrange
			var l = new MsgList();
			var m = new StringMsg(F.Rnd.Str);

			// Act
			l.Add<TestMsg>();
			l.Add<TestMsg>();
			l.Add(m);

			// Assert
			Assert.Equal(3, l.Count);
			Assert.Equal(2, l.Get<TestMsg>().Count);
		}

		private class TestMsg : IMsg { }

		public class StringMsg : Jm.WithValueMsg<string>
		{
			public StringMsg(string value) : base(value) { }
		}
	}
}
