// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.MsgList_Tests
{
	public class Add_Tests
	{
		[Fact]
		public void Adds_Message_To_List()
		{
			// Arrange
			var l = new MsgList();

			// Act
			l.Add<TestMsg>();
			l.Add(new TestMsg());

			// Assert
			Assert.Equal(2, l.Count);
		}

		private class TestMsg : IMsg { }
	}
}
