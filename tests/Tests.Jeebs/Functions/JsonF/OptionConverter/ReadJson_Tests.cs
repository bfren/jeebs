// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Xunit;
using static F.JsonF.M;

namespace F.JsonF_Tests.OptionConverter_Tests;

public class ReadJson_Tests
{
	[Fact]
	public void Deserialise_Returns_Object()
	{
		// Arrange
		var valueStr = Rnd.Str;
		var valueInt = Rnd.Int;
		var json = $"{{\"foo\":\"{valueStr}\",\"bar\":{valueInt}}}";
		var expected = new Test(valueStr, valueInt);

		// Act
		var result = JsonF.Deserialise<Maybe<Test>>(json);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(expected, some);
	}

	[Fact]
	public void Deserialise_Returns_Object_With_Property()
	{
		// Arrange
		var valueStr = Rnd.Str;
		var valueInt = Rnd.Int;
		var json = $"{{\"test\":{{\"foo\":\"{valueStr}\",\"bar\":{valueInt}}}}}";
		var expected = new Wrapper(new Test(valueStr, valueInt));

		// Act
		var result = JsonF.Deserialise<Wrapper>(json);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(expected, some);
	}

	[Theory]
	[InlineData("\"  \"")]
	[InlineData("true")]
	[InlineData("false")]
	[InlineData("[0,1,2]")]
	[InlineData("{\"bar\":\"foo\"}")]
	public void Deserialise_Null_Or_Invalid_Value_Returns_None_With_DeserialisingValueExceptionMsg(string input)
	{
		// Arrange

		// Act
		var result = JsonF.Deserialise<Maybe<Test>>(input);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<DeserialiseExceptionMsg>(none);
	}

	[Theory]
	[InlineData("\"  \"")]
	[InlineData("true")]
	[InlineData("false")]
	[InlineData("[0,1,2]")]
	[InlineData("{\"bar\":\"foo\"}")]
	public void Deserialise_Object_With_Maybe_Property_Null_Or_Invalid_Value_Returns_None_With_DeserialisingValueExceptionMsg(string input)
	{
		// Arrange
		var json = $"{{\"test\":{input}}}";

		// Act
		var result = JsonF.Deserialise<Wrapper>(json);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<DeserialiseExceptionMsg>(none);
	}

	public record class Test(string Foo, int Bar);

	public record class Wrapper(Maybe<Test> Test);
}
