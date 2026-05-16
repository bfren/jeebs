// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.Json;
using Jeebs.Collections;

namespace Jeebs.Internals.JsonConverters.PagedListJsonConverter_Tests;

public class WriteJson_Tests : Setup
{
	[Fact]
	public void Empty_PagedList_Writes_Object_With_Empty_Items_Array()
	{
		// Arrange
		var opt = GetOptions();
		var list = new PagedList<int>();

		// Act
		var json = JsonSerializer.Serialize(list, opt);

		// Assert
		Assert.StartsWith("{", json);
		Assert.EndsWith("}", json);
		Assert.Contains("\"values\":", json);
		Assert.Contains("\"items\":[]", json);
	}

	[Fact]
	public void Populated_PagedList_Writes_Items_And_Values()
	{
		// Arrange
		var opt = GetOptions();
		var values = new PagingValues(items: 10, page: 2, itemsPer: 5, pagesPer: 3);
		var list = new PagedList<int>(values, [6, 7, 8, 9, 10]);

		// Act
		var json = JsonSerializer.Serialize(list, opt);

		// Assert
		Assert.Contains("\"items\":[6,7,8,9,10]", json);
		Assert.Contains("\"page\":2", json);
		Assert.Contains("\"itemsPer\":5", json);
	}

	[Fact]
	public void Writes_All_PagingValues_Properties_In_CamelCase()
	{
		// Arrange
		var opt = GetOptions();
		var values = new PagingValues(items: 47, page: 3, itemsPer: 5, pagesPer: 4);
		var list = new PagedList<int>(values, [11, 12, 13, 14, 15]);

		// Act
		var json = JsonSerializer.Serialize(list, opt);

		// Assert
		Assert.Contains("\"items\":47", json);
		Assert.Contains("\"itemsPer\":5", json);
		Assert.Contains("\"firstItem\":", json);
		Assert.Contains("\"lastItem\":", json);
		Assert.Contains("\"page\":3", json);
		Assert.Contains("\"pages\":", json);
		Assert.Contains("\"pagesPer\":4", json);
		Assert.Contains("\"lowerPage\":", json);
		Assert.Contains("\"upperPage\":", json);
		Assert.Contains("\"skip\":", json);
		Assert.Contains("\"take\":", json);
	}

	[Fact]
	public void Writes_Values_When_IPagingValues_Is_A_Mock()
	{
		// Arrange
		var opt = GetOptions();
		var mock = Substitute.For<IPagingValues>();
		mock.Items.Returns(42ul);
		mock.ItemsPer.Returns(7ul);
		mock.Page.Returns(2ul);
		var list = new PagedList<int>(mock, [1, 2]);

		// Act
		var json = JsonSerializer.Serialize(list, opt);

		// Assert
		Assert.Contains("\"items\":42", json);
		Assert.Contains("\"itemsPer\":7", json);
		Assert.Contains("\"page\":2", json);
		Assert.Contains("\"items\":[1,2]", json);
	}

	[Fact]
	public void Writes_Items_Of_Complex_Type()
	{
		// Arrange
		var opt = GetOptions();
		var values = new PagingValues(items: 2, page: 1);
		var list = new PagedList<Foo>(values, [new Foo(1, "a"), new Foo(2, "b")]);

		// Act
		var json = JsonSerializer.Serialize(list, opt);

		// Assert
		Assert.Contains("\"items\":[{\"id\":1,\"name\":\"a\"},{\"id\":2,\"name\":\"b\"}]", json);
	}

	public sealed record class Foo(int Id, string Name);
}
