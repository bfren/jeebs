using System;
using System.Collections.Generic;
using System.Text;
using Jx.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.Map_Tests
{
	public class Validate_Tests
	{
		[Fact]
		public void Table_Missing_Column_Throws_InvalidTableMapException()
		{
			// Arrange
			var maps = Substitute.For<ITableMaps>();
			var table = new FooTableWithoutBar0();

			// Act
			void action() => Map<Foo>.To(table);

			// Assert
			var ex = Assert.Throws<InvalidTableMapException>(action);
			Assert.Equal($"The definition of table '{typeof(FooTableWithoutBar0).FullName}' is missing field 'Bar0'.", ex.Message);
		}

		[Fact]
		public void Entity_Missing_Property_Throws_InvalidTableMapException()
		{
			// Arrange
			var maps = Substitute.For<ITableMaps>();
			var table = new FooTableWithBar2();

			// Act
			void action() => Map<Foo>.To(table);

			// Assert
			var ex = Assert.Throws<InvalidTableMapException>(action);
			Assert.Equal($"The definition of entity '{typeof(Foo).FullName}' is missing property 'Bar2'.", ex.Message);
		}
	}
}
