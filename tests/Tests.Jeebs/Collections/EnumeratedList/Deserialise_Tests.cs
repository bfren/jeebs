// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.EnumeratedList_Tests;

public class Deserialise_Tests
{
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public void Null_Or_Empty_Returns_Empty_List(string input)
	{
		// Arrange

		// Act
		var result = EnumeratedList.Deserialise<Foo>(input);

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public void Invalid_Json_Returns_Empty_List()
	{
		// Arrange
		var json = F.Rnd.Str;

		// Act
		var result = EnumeratedList.Deserialise<Foo>(json);

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public void Incorrect_Json_Returns_Empty_List()
	{
		// Arrange
		const string? json = "{\"foo\":\"bar\"}";

		// Act
		var result = EnumeratedList.Deserialise<Foo>(json);

		// Assert
		Assert.Empty(result);
	}

	[Fact]
	public void Correct_Json_Returns_List()
	{
		// Arrange
		var itemA = new Foo(F.Rnd.Str);
		var itemB = new Foo(F.Rnd.Str);
		var itemC = new Foo(F.Rnd.Str);
		var itemD = new Foo(F.Rnd.Str);
		var json = $"[\"{itemB}\",\"{itemD}\",\"{itemA}\",\"{itemC}\"]";

		// Act
		var result = EnumeratedList.Deserialise<Foo>(json);

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
