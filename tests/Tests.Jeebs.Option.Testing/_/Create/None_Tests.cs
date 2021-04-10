// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static Jeebs.Create.Msg;

namespace Jeebs.Create_Tests
{
	public class None_Tests
	{
		[Fact]
		public void Creates_None_With_EmptyNoneForTestingMsg()
		{
			// Arrange

			// Act
			var result = Create.None<int>();

			// Assert
			var none = result.AssertNone();
			Assert.IsType<EmptyNoneForTestingMsg>(none);
		}
	}
}
