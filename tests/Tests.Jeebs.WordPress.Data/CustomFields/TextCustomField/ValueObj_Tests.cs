// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.WordPress.Data.CustomFields.TextCustomField_Tests
{
	public class ValueObj_Tests
	{
		[Fact]
		public void Returns_ValueStr_If_Not_Null()
		{
			// Arrange
			var value = F.Rnd.Str;
			var field = new Test(F.Rnd.Str, value);

			// Act
			var result = field.ValueObj;

			// Assert
			Assert.Equal(value, result);
		}

		[Fact]
		public void Returns_Empty_String_If_ValueStr_Is_Null()
		{
			// Arrange
			var field = new Test(F.Rnd.Str, null);

			// Act
			var result = field.ValueObj;

			// Assert
			Assert.Equal(string.Empty, result);
		}

		public record Test : TextCustomField
		{
			public Test(string Key, string? Value) : base(Key) =>
				ValueStr = Value;
		}
	}
}
