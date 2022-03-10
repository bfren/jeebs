﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Query.QueryParameters_Tests;

public class ToString_Tests
{
	[Fact]
	public void Returns_Json()
	{
		// Arrange
		var param = new QueryParametersDictionary();
		var p0 = Rnd.Str;
		var p1 = Rnd.Int;
		_ = param.TryAdd(new { p0, p1 });

		// Act
		var result = param.ToString();

		// Assert
		Assert.Equal($"{{\"{nameof(p0)}\":\"{p0}\",\"{nameof(p1)}\":{p1}}}", result);
	}
}