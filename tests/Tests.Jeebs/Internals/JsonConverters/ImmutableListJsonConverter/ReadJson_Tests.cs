// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json;
using Jeebs.Collections;

namespace Jeebs.Internals.JsonConverters.ImmutableListJsonConverter_Tests;

public class ReadJson_Tests : Setup
{
	[Fact]
	public void Deserialise_Array_Returns_ImmutableList()
	{
		// Arrange
		var opt = GetOptions();
		var json = /*lang=json,strict*/ "[1,2,3]";

		// Act
		var result = JsonSerializer.Deserialize<ImmutableList<int>>(json, opt);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(3, result.Count);
		Assert.Collection(result,
			x => Assert.Equal(1, x),
			x => Assert.Equal(2, x),
			x => Assert.Equal(3, x)
		);
	}

	[Fact]
	public void Deserialise_Empty_Array_Returns_Empty_ImmutableList()
	{
		// Arrange
		var opt = GetOptions();
		var json = /*lang=json,strict*/ "[]";

		// Act
		var result = JsonSerializer.Deserialize<ImmutableList<int>>(json, opt);

		// Assert
		Assert.NotNull(result);
		Assert.Empty(result);
	}

	[Fact]
	public void Deserialise_Null_Returns_Null()
	{
		// Arrange
		var opt = GetOptions();
		var json = "null";

		// Act
		var result = JsonSerializer.Deserialize<ImmutableList<int>>(json, opt);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void Deserialise_Non_Array_Token_Throws()
	{
		// Arrange
		var opt = GetOptions();
		var json = /*lang=json,strict*/ "{\"foo\":1}";

		// Act + Assert
		Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<ImmutableList<int>>(json, opt));
	}
}
