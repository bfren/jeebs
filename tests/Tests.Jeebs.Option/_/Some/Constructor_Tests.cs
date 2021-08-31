// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Internals;
using Xunit;

namespace Jeebs.Some_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Value()
		{
			// Arrange
			var value = F.Rnd.Str;

			// Act
			var result = new Some<string>(value);

			// Assert
			Assert.Equal(value, result.Value);
		}
	}
}
