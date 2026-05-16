// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json;
using Jeebs.Collections;

namespace Jeebs.Internals.JsonConverters.PagedListJsonConverter_Tests;

public class ReadJson_Tests : Setup
{
	[Fact]
	public void Deserialise_Object_Returns_PagedList_With_Values_And_Items()
	{
		// Arrange
		var opt = GetOptions();
		var json = /*lang=json,strict*/ "{\"values\":{\"items\":10,\"itemsPer\":5,\"page\":2},\"items\":[6,7,8,9,10]}";

		// Act
		var result = JsonSerializer.Deserialize<PagedList<int>>(json, opt);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(5, result.Count);
		Assert.Collection(result,
			x => Assert.Equal(6, x),
			x => Assert.Equal(7, x),
			x => Assert.Equal(8, x),
			x => Assert.Equal(9, x),
			x => Assert.Equal(10, x)
		);
		Assert.Equal(10ul, result.Values.Items);
		Assert.Equal(5ul, result.Values.ItemsPer);
		Assert.Equal(2ul, result.Values.Page);
	}

	[Fact]
	public void Deserialise_Empty_Items_Returns_Empty_PagedList()
	{
		// Arrange
		var opt = GetOptions();
		var json = /*lang=json,strict*/ "{\"values\":{\"items\":0,\"page\":1},\"items\":[]}";

		// Act
		var result = JsonSerializer.Deserialize<PagedList<int>>(json, opt);

		// Assert
		Assert.NotNull(result);
		Assert.Empty(result);
	}

	[Fact]
	public void Deserialise_Missing_Values_Returns_Default_PagingValues()
	{
		// Arrange
		var opt = GetOptions();
		var json = /*lang=json,strict*/ "{\"items\":[1,2,3]}";

		// Act
		var result = JsonSerializer.Deserialize<PagedList<int>>(json, opt);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(3, result.Count);
		Assert.Equal(new PagingValues(), result.Values);
	}

	[Fact]
	public void Deserialise_Missing_Items_Returns_Empty_List_With_Values()
	{
		// Arrange
		var opt = GetOptions();
		var json = /*lang=json,strict*/ "{\"values\":{\"items\":5,\"page\":1}}";

		// Act
		var result = JsonSerializer.Deserialize<PagedList<int>>(json, opt);

		// Assert
		Assert.NotNull(result);
		Assert.Empty(result);
		Assert.Equal(5ul, result.Values.Items);
	}

	[Fact]
	public void Deserialise_Ignores_Unknown_Properties()
	{
		// Arrange
		var opt = GetOptions();
		var json = /*lang=json,strict*/ "{\"values\":{\"page\":1},\"extra\":123,\"items\":[42]}";

		// Act
		var result = JsonSerializer.Deserialize<PagedList<int>>(json, opt);

		// Assert
		Assert.NotNull(result);
		Assert.Single(result);
		Assert.Equal(42, result[0]);
	}

	[Fact]
	public void Deserialise_Non_Object_Token_Throws()
	{
		// Arrange
		var opt = GetOptions();
		var json = /*lang=json,strict*/ "[1,2,3]";

		// Act + Assert
		Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<PagedList<int>>(json, opt));
	}

	[Fact]
	public void Deserialise_Null_Returns_Null()
	{
		// Arrange
		var opt = GetOptions();
		var json = "null";

		// Act
		var result = JsonSerializer.Deserialize<PagedList<int>>(json, opt);

		// Assert
		Assert.Null(result);
	}
}
