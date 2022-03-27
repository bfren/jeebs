// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Id;

namespace Jeebs.Functions.Internals.StrongIdConverter_Tests;

public class WriteJson_Tests
{
	[Fact]
	public void Serialise_Returns_Json_Value()
	{
		// Arrange
		var value = Rnd.Lng;
		var id = new TestId { Value = value };

		// Act
		var result = JsonF.Serialise(id);

		// Assert
		Assert.Equal($"\"{value}\"", result);
	}

	[Theory]
	[InlineData(null)]
	public void Serialise_Returns_Empty_Json(TestId? input)
	{
		// Arrange

		// Act
		var result = JsonF.Serialise(input);

		// Assert
		Assert.Equal(JsonF.Empty, result);
	}

	public sealed record class TestId : StrongId;
}
