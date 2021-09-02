// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.WordPress.Data.MetaDictionary_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Concatenates_Duplicate_Items()
		{
			// Arrange
			var k0 = F.Rnd.Str;
			var k1 = F.Rnd.Str;
			var v0 = F.Rnd.Str;
			var v1 = F.Rnd.Str;
			var v2 = F.Rnd.Str;

			// Act
			var result = new MetaDictionary(new KeyValuePair<string, string>[]
			{
				new(k0, v0),
				new(k0, v1),
				new(k1, v2)
			});

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
}
