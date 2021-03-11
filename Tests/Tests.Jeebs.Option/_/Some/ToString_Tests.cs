// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.OptionSome_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void With_Value_Returns_Value_ToString()
		{
			// Arrange
			var value = F.Rnd.Lng;
			var option = Option.Wrap(value);

			// Act
			var result = option.ToString();

			// Assert
			Assert.Equal(value.ToString(), result);
		}

		[Fact]
		public void Value_Is_Null_Returns_Type()
		{
			// Arrange
			int? value = null;
			var option = Option.Wrap(value, true);

			// Act
			var result = option.ToString();

			// Assert
			Assert.Equal("Some: " + typeof(int?).ToString(), result);
		}
	}
}
