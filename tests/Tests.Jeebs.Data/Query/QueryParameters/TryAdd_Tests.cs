// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Query.QueryParameters_Tests;

public class TryAdd_Tests
{
	[Theory]
	[InlineData(null)]
	[InlineData(42)]
	[InlineData(true)]
	[InlineData('c')]
	public void Ignores_Null_And_Primitive_Types(object input)
	{
		// Arrange
		var parameters = new QueryParametersDictionary();

		// Act
		var result = parameters.TryAdd(input);

		// Assert
		Assert.False(result);
		Assert.Empty(parameters);
	}

	[Fact]
	public void With_QueryParameters_Returns_False_If_Key_Exists()
	{
		// Arrange
		var p0 = Rnd.Str;
		var parameterToAdd = new QueryParametersDictionary
		{
			{ nameof(p0), p0 }
		};

		var parameters = new QueryParametersDictionary();
		_ = parameters.TryAdd(parameterToAdd);

		// Act
		var result = parameters.TryAdd(parameterToAdd);

		// Assert
		Assert.False(result);
		Assert.Collection(parameters,
			x => { Assert.Equal(nameof(p0), x.Key); Assert.Equal(p0, x.Value); }
		);
	}

	[Fact]
	public void Adds_QueryParameters_To_Dictionary()
	{
		// Arrange
		var p0 = Rnd.Str;
		var p1 = Rnd.Str;
		var p2 = Rnd.Str;
		var parametersToAdd = new QueryParametersDictionary
		{
			{ nameof(p0), p0 },
			{ nameof(p1), p1 },
			{ nameof(p2), p2 }
		};

		var parameters = new QueryParametersDictionary();

		// Act
		var result = parameters.TryAdd(parametersToAdd);

		// Assert
		Assert.True(result);
		Assert.Collection(parameters,
			x => { Assert.Equal(nameof(p0), x.Key); Assert.Equal(p0, x.Value); },
			x => { Assert.Equal(nameof(p1), x.Key); Assert.Equal(p1, x.Value); },
			x => { Assert.Equal(nameof(p2), x.Key); Assert.Equal(p2, x.Value); }
		);
	}

	[Fact]
	public void With_AnonymousType_Returns_False_If_Key_Exists()
	{
		// Arrange
		var p0 = Rnd.Str;
		var parameterToAdd = new { p0 };

		var parameters = new QueryParametersDictionary();
		_ = parameters.TryAdd(parameterToAdd);

		// Act
		var result = parameters.TryAdd(parameterToAdd);

		// Assert
		Assert.False(result);
		Assert.Collection(parameters,
			x => { Assert.Equal(nameof(p0), x.Key); Assert.Equal(p0, x.Value); }
		);
	}

	[Fact]
	public void Adds_AnonymousType_To_Dictionary()
	{
		// Arrange
		var p0 = Rnd.Str;
		var p1 = Rnd.Str;
		var p2 = Rnd.Str;
		var parametersToAdd = new { p0, p1, p2 };

		var parameters = new QueryParametersDictionary();

		// Act
		var result = parameters.TryAdd(parametersToAdd);

		// Assert
		Assert.True(result);
		Assert.Collection(parameters,
			x => { Assert.Equal(nameof(p0), x.Key); Assert.Equal(p0, x.Value); },
			x => { Assert.Equal(nameof(p1), x.Key); Assert.Equal(p1, x.Value); },
			x => { Assert.Equal(nameof(p2), x.Key); Assert.Equal(p2, x.Value); }
		);
	}

	[Fact]
	public void Adds_Type_With_Public_Properties_To_Dictionary()
	{
		// Arrange
		var parameters = new QueryParametersDictionary();
		var foo = new Foo();

		// Act
		var result = parameters.TryAdd(foo);

		// Assert
		Assert.True(result);
		Assert.Collection(parameters,
			x => { Assert.Equal(nameof(Foo.Bar0), x.Key); Assert.Equal(42, x.Value); }
		);
	}

	public class Foo
	{
		public int Bar0 { get; set; } = 42;

		public string Bar1 { private get; set; } = Rnd.Str;
	}
}
