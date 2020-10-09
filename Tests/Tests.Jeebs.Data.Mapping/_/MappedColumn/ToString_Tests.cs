using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.MappedColumn_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void Returns_Name()
		{
			// Arrange
			var table = F.Rnd.String;
			var name = F.Rnd.String;
			var prop = Substitute.For<PropertyInfo>();
			prop.Name.Returns(F.Rnd.String);
			var column = new MappedColumn(table, name, prop);

			// Act
			var result = column.ToString();

			// Assert
			Assert.Equal(name, result);
		}
	}
}
