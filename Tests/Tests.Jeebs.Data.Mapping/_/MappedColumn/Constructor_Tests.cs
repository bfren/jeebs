using NSubstitute;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace Jeebs.Data.Mapping.MappedColumn_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Properties_Set()
		{
			// Arrange
			var table = F.StringF.Random(6);
			var name = F.StringF.Random(6);
			var alias = F.StringF.Random(6);
			var prop = Substitute.For<PropertyInfo>();
			prop.Name.Returns(alias);

			// Act
			var result = new MappedColumn(table, name, prop);

			// Assert
			Assert.Equal(table, result.Table);
			Assert.Equal(name, result.Name);
			Assert.Equal(alias, result.Alias);
			Assert.Equal(prop, result.Property);
		}
	}
}
