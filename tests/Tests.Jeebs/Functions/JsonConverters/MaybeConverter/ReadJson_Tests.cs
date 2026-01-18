// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions.JsonConverters.MaybeConverter_Tests;

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
		result.AssertOk(expected);
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
		result.AssertOk(expected);
	}

	[Theory]
	[InlineData("\"  \"")]
	[InlineData("true")]
	[InlineData("false")]
	[InlineData("[0,1,2]")]
	[InlineData(/*lang=json,strict*/ "{\"bar\":\"foo\"}")]
	public void Deserialise_Null_Or_Invalid_Value_Returns_Fail(string input)
	{
		// Arrange

		// Act
		var result = JsonF.Deserialise<Maybe<Test>>(input);

		// Assert
		var fail = result.AssertFail();
		Assert.NotNull(fail.Exception);
	}

	[Theory]
	[InlineData("\"  \"")]
	[InlineData("true")]
	[InlineData("false")]
	[InlineData("[0,1,2]")]
	[InlineData(/*lang=json,strict*/ "{\"bar\":\"foo\"}")]
	public void Deserialise_Object_With_Maybe_Property_Null_Or_Invalid_Value_Returns_Fail(string input)
	{
		// Arrange
		var json = $"{{\"test\":{input}}}";

		// Act
		var result = JsonF.Deserialise<Wrapper>(json);

		// Assert
		var fail = result.AssertFail();
		Assert.NotNull(fail.Exception);
	}

	public record class Test(string Foo, int Bar);

	public record class Wrapper(Maybe<Test> Test);
}
