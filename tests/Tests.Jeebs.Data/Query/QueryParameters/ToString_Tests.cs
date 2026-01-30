// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.QueryBuilder.QueryParameters_Tests;

public class ToString_Tests
{
	[Fact]
	public void No_Items__Returns_Empty()
	{
		// Arrange
		var param = new QueryParametersDictionary();

		// Act
		var result = param.ToString();

		// Assert
		Assert.Equal("(Empty)", result);
	}

	[Fact]
	public void With_Items__Returns_String()
	{
		// Arrange
		var param = new QueryParametersDictionary();
		var p0 = Rnd.Str;
		var p1 = Rnd.Int;
		param.TryAdd(new { p0, p1 });

		// Act
		var result = param.ToString();

		// Assert
		Assert.Equal($"2, {nameof(p0)} = {p0}, {nameof(p1)} = {p1}", result);
	}
}
