using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.Column_AliasComparer_Tests
{
	public class Equals_Tests
	{
		[Fact]
		public void Equal_Alias_Returns_True()
		{
			// Arrange
			var alias = "alias";

			var c0 = Substitute.For<IColumn>();
			c0.Name.Returns(F.Rnd.String);
			c0.Alias.Returns(alias);

			var c1 = Substitute.For<IColumn>();
			c1.Name.Returns(F.Rnd.String);
			c1.Alias.Returns(alias);

			var comparer = new Column.AliasComparer();

			// Act

			// Assert
			Assert.Equal(c0, c1, comparer);
		}

		[Fact]
		public void Not_Equal_Alias_Returns_False()
		{
			// Arrange
			var c0 = Substitute.For<IColumn>();
			c0.Alias.Returns(F.Rnd.String);

			var c1 = Substitute.For<IColumn>();
			c1.Alias.Returns(F.Rnd.String);

			var comparer = new Column.AliasComparer();

			// Act

			// Assert
			Assert.NotEqual(c0, c1, comparer);
		}
	}
}
