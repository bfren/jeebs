// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;
using static F.DataF.MappingF;

namespace Jeebs.Data.Mapping.Mapper_Tests
{
	public class ValidateTable_Tests
	{
		[Fact]
		public void Table_Missing_Column_Returns_Invalid_With_Error()
		{
			// Arrange
			var e0 = $"The definition of table '{typeof(FooTableWithoutBar0).FullName}' is missing field '{nameof(Foo.Bar0)}'.";

			// Act
			var (valid, errors) = ValidateTable<Foo>(new FooTableWithoutBar0());

			// Assert
			Assert.False(valid);
			Assert.Collection(errors, x => Assert.Equal(e0, x));
		}

		[Fact]
		public void Table_Missing_Columns_Returns_Invalid_With_Errors()
		{
			// Arrange
			var e0 = $"The definition of table '{typeof(FooTableWithoutAny).FullName}' is missing field '{nameof(Foo.FooId)}'.";
			var e1 = $"The definition of table '{typeof(FooTableWithoutAny).FullName}' is missing field '{nameof(Foo.Bar0)}'.";
			var e2 = $"The definition of table '{typeof(FooTableWithoutAny).FullName}' is missing field '{nameof(Foo.Bar1)}'.";

			// Act
			var (valid, errors) = ValidateTable<Foo>(new FooTableWithoutAny());

			// Assert
			Assert.False(valid);
			Assert.Collection(errors,
				x => Assert.Equal(e0, x),
				x => Assert.Equal(e1, x),
				x => Assert.Equal(e2, x)
			);
		}

		[Fact]
		public void Entity_Missing_Property_Returns_Invalid_With_Error()
		{
			// Arrange
			var e0 = $"The definition of entity '{typeof(Foo).FullName}' is missing property '{nameof(FooTableWithBar2.Bar2)}'.";

			// Act
			var (valid, errors) = ValidateTable<Foo>(new FooTableWithBar2());

			// Assert
			Assert.False(valid);
			Assert.Collection(errors, x => Assert.Equal(e0, x));
		}

		[Fact]
		public void Entity_Missing_Properties_Returns_Invalid_With_Errors()
		{
			// Arrange
			var e0 = $"The definition of entity '{typeof(Foo).FullName}' is missing property '{nameof(FooTableWithBar234.Bar2)}'.";
			var e1 = $"The definition of entity '{typeof(Foo).FullName}' is missing property '{nameof(FooTableWithBar234.Bar3)}'.";
			var e2 = $"The definition of entity '{typeof(Foo).FullName}' is missing property '{nameof(FooTableWithBar234.Bar4)}'.";

			// Act
			var (valid, errors) = ValidateTable<Foo>(new FooTableWithBar234());

			// Assert
			Assert.False(valid);
			Assert.Collection(errors,
				x => Assert.Equal(e0, x),
				x => Assert.Equal(e1, x),
				x => Assert.Equal(e2, x)
			);
		}
	}
}
