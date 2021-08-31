// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Internals;
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

	public record class TestMsg : IMsg { }
}
