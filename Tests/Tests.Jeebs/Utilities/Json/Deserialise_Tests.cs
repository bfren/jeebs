using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Xunit;

namespace Jeebs.Util
{
	public partial class Json_Tests
	{
		[Theory]
		[InlineData(null)]
		public void Deserialise_Null_Returns_None(string input)
		{
			// Arrange

			// Act
			var result = Json.Deserialise<Test>(input);

			// Assert
			var none = Assert.IsAssignableFrom<None<Test>>(result);
			Assert.True(none.Reason is Jm.Util.Json.DeserialisingNullStringMsg);
		}

		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("\n")]
		public void Deserialise_Whitespace_Returns_None(string input)
		{
			// Arrange

			// Act
			var result = Json.Deserialise<Test>(input);

			// Assert
			var none = Assert.IsAssignableFrom<None<Test>>(result);
			Assert.True(none.Reason is Jm.Util.Json.DeserialisingReturnedNullMsg);
		}

		[Fact]
		public void Deserialise_InvalidJson_Returns_None()
		{
			// Arrange
			const string input = "this is not valid json";

			// Act
			var result = Json.Deserialise<Test>(input);

			// Assert
			var none = Assert.IsAssignableFrom<None<Test>>(result);
			Assert.True(none.Reason is Jm.Util.Json.DeserialiseExceptionMsg);
		}

		[Fact]
		public void Deserialise_ValidJson_ReturnsObject()
		{
			// Arrange
			const string input = "{\"foo\":\"test\",\"bar\":2,\"ignore\":\"this\"}";
			var expected = new Test { Foo = "test", Bar = 2 };

			// Act
			var result = Json.Deserialise<Test>(input).Unwrap(() => new Test());

			// Assert
			Assert.Equal(expected, result, new TestComparer());
		}
	}
}
