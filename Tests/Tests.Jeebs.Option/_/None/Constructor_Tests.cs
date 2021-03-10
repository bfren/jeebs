// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.None_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Reason()
		{
			// Arrange
			var reason = new TestMsg();

			// Act
			var result = new None<string>(reason);

			// Assert
			Assert.Equal(reason, result.Reason);
		}
	}

	public record TestMsg : IMsg { }
}
