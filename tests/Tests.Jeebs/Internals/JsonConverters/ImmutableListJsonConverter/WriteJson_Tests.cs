// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json;
using Jeebs.Collections;

namespace Jeebs.Internals.JsonConverters.ImmutableListJsonConverter_Tests;

public class WriteJson_Tests : Setup
{
	[Fact]
	public void Empty_List_Writes_Empty_Array()
	{
		// Arrange
		var opt = GetOptions();
		var list = new ImmutableList<int>();
		var expected = /*lang=json,strict*/ "{\"items\":[]}";

		// Act
		var json = JsonSerializer.Serialize(list, opt);

		// Assert
		Assert.Equal(expected, json);
	}

	[Fact]
	public void Populated_List_Writes_Json_Array_In_Order()
	{
		// Arrange
		var opt = GetOptions();
		var list = new ImmutableList<int>([3, 1, 4, 1, 5]);
		var expected = /*lang=json,strict*/ "{\"items\":[3,1,4,1,5]}";

		// Act
		var json = JsonSerializer.Serialize(list, opt);

		// Assert
		Assert.Equal(expected, json);
	}
}
