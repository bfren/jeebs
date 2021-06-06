// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.CustomField_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Properties()
		{
			// Arrange
			var key = F.Rnd.Str;
			var value = F.Rnd.Int;

			// Act
			var result = Substitute.ForPartsOf<CustomField<int>>(key, value);

			// Assert
			Assert.Equal(key, result.Key);
			Assert.Equal(value, result.ValueObj);
		}
	}
}
