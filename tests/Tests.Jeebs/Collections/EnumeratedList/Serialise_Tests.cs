// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;

namespace Jeebs.Collections.EnumeratedList_Tests;

public class Serialise_Tests
{
	[Fact]
	public void Empty_List_Returns_Empty_Json()
	{
		// Arrange
		var list = new EnumeratedList<Foo>();

		// Act
		var result = list.Serialise();

		// Assert
		Assert.Equal(JsonF.Empty, result);
	}

	[Fact]
	public void List_Returns_Json()
	{
		// Arrange
		var list = new EnumeratedList<Foo> { { Foo.A }, { Foo.B } };
		var json = $"[\"{Foo.A}\",\"{Foo.B}\"]";

		// Act
		var result = list.Serialise();

		// Assert
		Assert.Equal(json, result);
	}

	public record class Foo : Enumerated
	{
		public Foo(string name) : base(name) { }

		public static readonly Foo A = new(Rnd.Str);

		public static readonly Foo B = new(Rnd.Str);
	}
}
