// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Xunit;

namespace F.JsonF_Tests.StrongIdConverter_Tests;

public class ReadJson_Tests
{
	[Theory]
	[InlineData("{0}")]
	[InlineData("\"{0}\"")]
	public void Deserialise_Valid_Json_Returns_LongId_With_Value(string format)
	{
		// Arrange
		var value = Rnd.Lng;
		var json = string.Format(format, value);

		// Act
		var result = JsonF.Deserialise<IdTest0>(json);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some.Value);
	}

	[Theory]
	[InlineData("{0}")]
	[InlineData("\"{0}\"")]
	public void Deserialise_Object_With_LongId_Property_Returns_Object(string format)
	{
		// Arrange
		var v0 = Rnd.Int;
		var v1 = Rnd.Lng;
		var json = $"{{ \"id\": {v0}, \"longId\": {string.Format(format, v1)} }}";

		// Act
		var result = JsonF.Deserialise<LongIdWrapperTest0>(json);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(v0, some.Id);
		Assert.Equal(v1, some.LongId.Value);
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
		var result = JsonF.Deserialise<IdTest0>(input);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(0U, some.Value);
	}

	[Theory]
	[InlineData(JsonF.Empty)]
	[InlineData("\"  \"")]
	[InlineData("true")]
	[InlineData("false")]
	[InlineData("[ 0, 1, 2 ]")]
	[InlineData("{ \"foo\": \"bar\" }")]
	public void Deserialise_Object_With_LongId_Property_Null_Or_Invalid_Value_Returns_Object(string input)
	{
		// Arrange
		var v0 = Rnd.Int;
		var json = $"{{ \"id\": {v0}, \"longId\": {input} }}";

		// Act
		var result = JsonF.Deserialise<LongIdWrapperTest0>(json);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(v0, some.Id);
		Assert.Equal(0U, some.LongId.Value);
	}

	public readonly record struct IdTest0(long Value) : IStrongId;

	public class LongIdWrapperTest0
	{
		public int Id { get; set; }

		public IdTest0 LongId { get; set; } = new();
	}
}
