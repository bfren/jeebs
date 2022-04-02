// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Map.Column_AliasComparer_Tests;

public class GetHashCode_Tests
{
	[Fact]
	public void Returns_Alias_Hash()
	{
		// Arrange
		var alias = Rnd.Str;
		var ha = alias.GetHashCode();

		var c0 = Substitute.For<IColumn>();
		c0.ColName.Returns(Rnd.Str);
		c0.ColAlias.Returns(alias);

		var c1 = Substitute.For<IColumn>();
		c1.ColName.Returns(Rnd.Str);
		c1.ColAlias.Returns(alias);

		var comparer = new Column.AliasComparer();

		// Act
		var h0 = comparer.GetHashCode(c0);
		var h1 = comparer.GetHashCode(c1);

		// Assert
		Assert.Equal(ha, h0);
		Assert.Equal(ha, h1);
	}
}
