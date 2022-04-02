// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.MetaDictionary_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Concatenates_Duplicate_Items()
	{
		// Arrange
		var k0 = Rnd.Str;
		var k1 = Rnd.Str;
		var v0 = Rnd.Str;
		var v1 = Rnd.Str;
		var v2 = Rnd.Str;
		var items = new[]
		{
			new KeyValuePair<string, string>(k0, v0),
			new KeyValuePair<string, string>(k0, v1),
			new KeyValuePair<string, string>(k1, v2)
		};

		// Act
		var result = new MetaDictionary(items);

		// Assert
		Assert.Collection(result,
			x =>
			{
				Assert.Equal(k0, x.Key);
				Assert.Equal($"{v0};{v1}", x.Value);
			},
			x =>
			{
				Assert.Equal(k1, x.Key);
				Assert.Equal(v2, x.Value);
			}
		);
	}
}
