// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions.JsonConverters.MaybeConverter_Tests;

public class WriteJson_Tests
{
	[Fact]
	public void Serialise_Some_Returns_Value_As_Json()
	{
		// Arrange
		var valueStr = Rnd.Str;
		var valueInt = Rnd.Int;
		var value = new Test(valueStr, valueInt);
		var maybe = M.Wrap(value);
		var json = $"{{\"foo\":\"{valueStr}\",\"bar\":{valueInt}}}";

		// Act
		var result = JsonF.Serialise(maybe);

		// Assert
		var ok = result.AssertOk();
		Assert.Equal(json, ok);
	}

	[Fact]
	public void Serialise_None_Returns_Empty_Json()
	{
		// Arrange
		var maybe = M.None;

		// Act
		var result = JsonF.Serialise(maybe);

		// Assert
		var ok = result.AssertOk();
		Assert.Equal(JsonF.Empty, ok);
	}

	public record class Test(string Foo, int Bar);
}
