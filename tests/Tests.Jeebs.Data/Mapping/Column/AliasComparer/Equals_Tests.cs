// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.Column_AliasComparer_Tests;

public class Equals_Tests
{
	[Fact]
	public void Equal_Alias_Returns_True()
	{
		// Arrange
		var alias = F.Rnd.Str;

		var c0 = Substitute.For<IColumn>();
		_ = c0.ColName.Returns(F.Rnd.Str);
		_ = c0.ColAlias.Returns(alias);

		var c1 = Substitute.For<IColumn>();
		_ = c1.ColName.Returns(F.Rnd.Str);
		_ = c1.ColAlias.Returns(alias);

		var comparer = new Column.AliasComparer();

		// Act
		var result = comparer.Equals(c0, c1);

		// Assert
		Assert.True(result);
		Assert.Equal(c0, c1, comparer);
	}

	[Fact]
	public void Not_Equal_Alias_Returns_False()
	{
		// Arrange
		var c0 = Substitute.For<IColumn>();
		_ = c0.ColAlias.Returns(F.Rnd.Str);

		var c1 = Substitute.For<IColumn>();
		_ = c1.ColAlias.Returns(F.Rnd.Str);

		var comparer = new Column.AliasComparer();

		// Act
		var result = comparer.Equals(c0, c1);

		// Assert
		Assert.False(result);
		Assert.NotEqual(c0, c1, comparer);
	}
}
