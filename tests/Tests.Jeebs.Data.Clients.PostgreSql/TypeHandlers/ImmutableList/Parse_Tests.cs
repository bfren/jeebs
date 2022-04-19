// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Clients.PostgreSql.TypeHandlers.ImmutableList_Tests;

public class Parse_Tests
{
	[Theory]
	[InlineData("")]
	[InlineData(" ")]
	public void Empty_Returns_Empty_List(string input)
	{
		// Arrange
		var handler = new ImmutableListJsonbTypeHandler<Foo>();

		// Act
		var result = handler.Parse(input);

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public void Invalid_Json_Returns_Empty_List()
	{
		// Arrange
		var handler = new ImmutableListJsonbTypeHandler<Foo>();
		var json = Rnd.Str;

		// Act
		var result = handler.Parse(json);

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public void Incorrect_Json_Returns_Empty_List()
	{
		// Arrange
		var handler = new ImmutableListJsonbTypeHandler<Foo>();
		const string? json = /*lang=json,strict*/ "{\"foo\":\"bar\"}";

		// Act
		var result = handler.Parse(json);

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public void Correct_Json_Returns_List()
	{
		// Arrange
		var handler = new ImmutableListJsonbTypeHandler<Foo>();
		var itemA = new Foo(Rnd.Str);
		var itemB = new Foo(Rnd.Str);
		var itemC = new Foo(Rnd.Str);
		var itemD = new Foo(Rnd.Str);
		var json = $"[\"{itemB}\",\"{itemD}\",\"{itemA}\",\"{itemC}\"]";

		// Act
		var result = handler.Parse(json);

		// Assert
		Assert.Collection(result,
			b => Assert.Equal(itemB, b),
			d => Assert.Equal(itemD, d),
			a => Assert.Equal(itemA, a),
			c => Assert.Equal(itemC, c)
		);
	}

	public record class Foo : Enumerated
	{
		public Foo(string name) : base(name) { }
	}
}
