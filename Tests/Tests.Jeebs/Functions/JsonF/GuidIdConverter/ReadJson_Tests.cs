// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using Xunit;

namespace JeebsF.JsonF_Tests.GuidIdConverter_Tests
{
	public class ReadJson_Tests
	{
		[Fact]
		public void Deserialise_Valid_Json_Returns_GuidId_With_Value()
		{
			// Arrange
			var value = Guid.NewGuid();
			var json = $"\"{value}\"";

			// Act
			var result = JsonF.Deserialise<GuidIdTest0>(json);

			// Assert
			var some = Assert.IsType<Some<GuidIdTest0>>(result);
			Assert.Equal(value, some.Value.Value);
		}

		[Fact]
		public void Deserialise_Object_With_GuidId_Property_Returns_Object()
		{
			// Arrange
			var v0 = Rnd.Int;
			var v1 = Guid.NewGuid();
			var json = $"{{ \"id\": {v0}, \"guidId\": \"{v1}\" }}";

			// Act
			var result = JsonF.Deserialise<GuidIdWrapperTest0>(json);

			// Assert
			var wrapper = Assert.IsType<Some<GuidIdWrapperTest0>>(result).Value;
			Assert.Equal(v0, wrapper.Id);
			Assert.Equal(v1, wrapper.GuidId.Value);
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
			var result = JsonF.Deserialise<GuidIdTest0>(input);

			// Assert
			var some = Assert.IsType<Some<GuidIdTest0>>(result);
			Assert.Equal(Guid.Empty, some.Value.Value);
		}

		[Theory]
		[InlineData(JsonF.Empty)]
		[InlineData("\"  \"")]
		[InlineData("true")]
		[InlineData("false")]
		[InlineData("[ 0, 1, 2 ]")]
		[InlineData("{ \"foo\": \"bar\" }")]
		public void Deserialise_Object_With_GuidId_Property_Null_Or_Invalid_Value_Returns_Object(string input)
		{
			// Arrange
			var v0 = Rnd.Int;
			var json = $"{{ \"id\": {v0}, \"guidId\": {input} }}";

			// Act
			var result = JsonF.Deserialise<GuidIdWrapperTest0>(json);

			// Assert
			var wrapper = Assert.IsType<Some<GuidIdWrapperTest0>>(result).Value;
			Assert.Equal(v0, wrapper.Id);
			Assert.Equal(Guid.Empty, wrapper.GuidId.Value);
		}

		public record GuidIdTest0 : Jeebs.Id.GuidId;

		public class GuidIdWrapperTest0
		{
			public int Id { get; set; }

			public GuidIdTest0 GuidId { get; set; } = new GuidIdTest0();
		}
	}
}
