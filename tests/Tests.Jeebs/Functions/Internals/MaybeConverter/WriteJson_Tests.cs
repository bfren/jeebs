// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using MaybeF;

namespace Jeebs.Functions.Internals.MaybeConverter_Tests;

public class WriteJson_Tests
{
	[Fact]
	public void Serialise_Some_Returns_Some_Value_As_Json()
	{
		// Arrange
		var valueStr = Rnd.Str;
		var valueInt = Rnd.Int;
		var value = new Test(valueStr, valueInt);
		var maybe = F.Some(value);
		var json = $"{{\"foo\":\"{valueStr}\",\"bar\":{valueInt}}}";

		// Act
		var result = JsonF.Serialise(maybe);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(json, some);
	}

	[Fact]
	public void Serialise_None_Returns_Empty_Json()
	{
		// Arrange
		var maybe = Create.None<int>();

		// Act
		var result = JsonF.Serialise(maybe);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(JsonF.Empty, some);
	}

	public record class Test(string Foo, int Bar);
}
