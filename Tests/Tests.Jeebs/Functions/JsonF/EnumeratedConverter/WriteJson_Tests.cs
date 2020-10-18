using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace F.JsonF_Tests
{
	public class WriteJson_Tests
	{
		[Fact]
		public void Serialise_Enumerated_Returns_Json_Value()
		{
			// Arrange
			var value = Rnd.Str;
			var enumerated = new Test(value);

			// Act
			var result = JsonF.Serialise(enumerated);

			// Assert
			Assert.Equal($"\"{value}\"", result);
		}

		[Theory]
		[InlineData(null)]
		public void Serialise_Null_Enumerated_Returns_Empty_Json(Enumerated input)
		{
			// Arrange

			// Act
			var result = JsonF.Serialise(input);

			// Assert
			Assert.Equal(JsonF.Empty, result);
		}

		public class Test : Enumerated
		{
			public Test(string name) : base(name) { }
		}
	}
}
