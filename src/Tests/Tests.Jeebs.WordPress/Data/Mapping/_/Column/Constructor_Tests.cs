// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.WordPress.Data.Mapping.Column_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Properties_Set()
		{
			// Arrange
			var table = F.Rnd.Str;
			var name = F.Rnd.Str;
			var alias = F.Rnd.Str;
			var column = new Column(table, name, alias);

			// Act

			// Assert
			Assert.Equal(table, column.Table);
			Assert.Equal(name, column.Name);
			Assert.Equal(alias, column.Alias);
		}
	}
}
