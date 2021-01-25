using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Jeebs;
using Xunit;

namespace F.JsonF_Tests.IntIdConverter_Tests
{
	public class ReadJson_Tests
	{
		[Theory]
		[InlineData("{0}")]
		[InlineData("\"{0}\"")]
		public void Deserialise_Valid_Json_Returns_IntId_With_Value(string format)
		{
			// Arrange
			var value = Rnd.Lng;
			var json = string.Format(format, value);

			// Act
			var result = JsonF.Deserialise<IntIdTest0>(json);

			// Assert
			var some = Assert.IsType<Some<IntIdTest0>>(result);
			Assert.Equal(value, some.Value.Value);
		}

		[Theory]
		[InlineData("{0}")]
		[InlineData("\"{0}\"")]
		public void Deserialise_Object_With_IntId_Property_Returns_Object(string format)
		{
			// Arrange
			var v0 = Rnd.Int;
			var v1 = Rnd.Lng;
			var json = $"{{ \"id\": {v0}, \"intId\": {string.Format(format, v1)} }}";

			// Act
			var result = JsonF.Deserialise<IntIdWrapperTest0>(json);

			// Assert
			var wrapper = Assert.IsType<Some<IntIdWrapperTest0>>(result).Value;
			Assert.Equal(v0, wrapper.Id);
			Assert.Equal(v1, wrapper.IntId.Value);
		}

		[Theory]
		[InlineData(JsonF.Empty)]
		[InlineData("\"  \"")]
		[InlineData("true")]
		[InlineData("false")]
		public void Deserialise_Null_Or_Invalid_Value_Returns_Default(string input)
		{
			// Arrange

			// Act
			var result = JsonF.Deserialise<IntIdTest0>(input);

			// Assert
			var some = Assert.IsType<Some<IntIdTest0>>(result);
			Assert.Equal(0, some.Value.Value);
		}

		[Theory]
		[InlineData(JsonF.Empty)]
		[InlineData("\"  \"")]
		[InlineData("true")]
		[InlineData("false")]
		[InlineData("[ 0, 1, 2 ]")]
		[InlineData("{ \"foo\": \"bar\" }")]
		public void Deserialise_Object_With_IntId_Property_Null_Or_Invalid_Value_Returns_Object(string input)
		{
			// Arrange
			var v0 = Rnd.Int;
			var json = $"{{ \"id\": {v0}, \"intId\": {input} }}";

			// Act
			var result = JsonF.Deserialise<IntIdWrapperTest0>(json);

			// Assert
			var wrapper = Assert.IsType<Some<IntIdWrapperTest0>>(result).Value;
			Assert.Equal(v0, wrapper.Id);
			Assert.Equal(0, wrapper.IntId.Value);
		}

		public record IntIdTest0 : IntId;

		public class IntIdWrapperTest0
		{
			public int Id { get; set; }

			public IntIdTest0 IntId { get; set; } = new IntIdTest0();
		}
	}
}
