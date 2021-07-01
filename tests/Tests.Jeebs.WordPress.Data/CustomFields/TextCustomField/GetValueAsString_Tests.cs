// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.WordPress.Data.CustomFields.TextCustomField_Tests
{
	public class GetValueAsString_Tests
	{
		[Fact]
		public void Returns_ValueObj()
		{
			// Arrange
			var value = F.Rnd.Str;
			var field = new Test(F.Rnd.Str, value);

			// Act
			var result = field.GetValueAsStringTest();

			// Assert
			Assert.Equal(value, result);
		}

		public class Test : TextCustomField
		{
			public Test(string key, string value) : base(key) =>
				ValueObj = value;
		}
	}
}
