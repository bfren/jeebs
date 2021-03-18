// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace F.StringF_Tests
{
	public class Format_Tests
	{
		[Fact]
		public void Returns_IfNull_If_Object_Is_Null()
		{
			// Arrange
			object? obj = null;
			var format = Rnd.Str;
			var value = Rnd.Str;

			// Act
			var r0 = StringF.Format(format, obj);
			var r1 = StringF.Format(format, obj, value);

			// Assert
			Assert.Null(r0);
			Assert.Equal(value, r1);
		}

		[Fact]
		public void Returns_String_Format_If_Object_Is_Not_Null()
		{
			// Arrange
			const string format = "[{0}]";
			var value = Rnd.Str;

			// Act
			var result = StringF.Format(format, value);

			// Assert
			Assert.Equal(string.Format(format, value), result);
		}
	}
}
