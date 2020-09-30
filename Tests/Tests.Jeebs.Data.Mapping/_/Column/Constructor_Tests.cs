using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Data.Mapping.Column_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Properties_Set()
		{
			// Arrange
			var table = "table";
			var name = "name";
			var alias = "alias";
			var column = new Column(table, name, alias);

			// Act

			// Assert
			Assert.Equal(table, column.Table);
			Assert.Equal(name, column.Name);
			Assert.Equal(alias, column.Alias);
		}
	}
}
