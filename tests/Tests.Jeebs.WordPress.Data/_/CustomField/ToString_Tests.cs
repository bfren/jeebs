// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.CustomField_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void ValueObj_Not_Null_Returns_ValueObj_ToString()
		{
			// Arrange
			var key = F.Rnd.Str;
			var value = F.Rnd.Guid;
			var field = Substitute.ForPartsOf<CustomField<Guid>>(key, value);

			// Act
			var result = field.ToString();

			// Assert
			Assert.Equal(value.ToString(), result);
		}

		[Fact]
		public void ValueObj_Null_Returns_ValueStr()
		{
			// Arrange
			var key = F.Rnd.Str;
			Guid? value = null;
			string str = F.Rnd.Str;
			var field = Substitute.ForPartsOf<Test>(key, value, str);

			// Act
			var result = field.ToString();

			// Assert
			Assert.Equal(str, result);
		}

		[Fact]
		public void ValueObj_And_ValueStr_Null_Returns_Key()
		{
			// Arrange
			var key = F.Rnd.Str;
			Guid? value = null;
			var field = Substitute.ForPartsOf<CustomField<Guid?>>(key, value);

			// Act
			var result = field.ToString();

			// Assert
			Assert.Equal(key, result);
		}

		public abstract class Test : CustomField<Guid?>
		{
			protected Test(string key, Guid? value, string str) : base(key, value) =>
				ValueStr = str;
		}
	}
}
