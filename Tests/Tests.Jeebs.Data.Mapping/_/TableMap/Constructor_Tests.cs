using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.TableMap_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Properties_Set()
		{
			// Arrange
			var name = F.StringF.Random(6);
			var columns = Substitute.For<List<MappedColumn>>();

			var prop = Substitute.For<PropertyInfo>();
			prop.Name.Returns(F.StringF.Random(6));
			var idColumn = new MappedColumn(Arg.Any<string>(), Arg.Any<string>(), prop);

			// Act
			var map = new TableMap(name, columns, idColumn);

			// Assert
			Assert.Equal(name, map.Name);
			Assert.Equal(columns, map.Columns);
			Assert.Equal(idColumn, idColumn);
		}
	}
}
