// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.MsgList_Tests
{
	public class Count_Tests
	{
		[Fact]
		public void Returns_Zero_For_No_Messages()
		{
			// Arrange
			var l = new MsgList();

			// Act

			// Assert
			Assert.Equal(0, l.Count);
		}
	}
}
