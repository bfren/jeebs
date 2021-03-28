// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Reflection;
using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.Mapping.TableMap_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Properties_Set()
		{
			// Arrange
			var name = F.Rnd.Str;
			var columns = Substitute.For<IMappedColumnList>();

			var prop = Substitute.For<PropertyInfo>();
			prop.Name.Returns(F.Rnd.Str);
			var idColumn = Substitute.For<IMappedColumn>();
			idColumn.Property.Returns(prop);

			// Act
			var map = new TableMap(name, columns, idColumn);

			// Assert
			Assert.Equal(name, map.Name);
			Assert.Equal(columns, map.Columns);
			Assert.Equal(idColumn, idColumn);
		}
	}
}
