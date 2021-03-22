// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Data.Mapping.MapService_Tests
{
	public class ValidateTable_Tests
	{
		[Fact]
		public void Table_Missing_Column_Returns_Invalid_With_Error()
		{
			// Arrange
			using var svc = new MapService();

			// Act
			var (valid, errors) = svc.ValidateTable<Foo, FooTableWithoutBar0>();

			// Assert
			Assert.False(valid);
			Assert.Equal(
				$"The definition of table '{typeof(FooTableWithoutBar0).FullName}' is missing field '{nameof(Foo.Bar0)}'.",
				errors
			);
		}

		[Fact]
		public void Table_Missing_Columns_Returns_Invalid_With_Errors()
		{
			// Arrange
			using var svc = new MapService();

			// Act
			var (valid, errors) = svc.ValidateTable<Foo, FooTableWithoutAny>();

			// Assert
			Assert.False(valid);
			Assert.Equal(
				$"The definition of table '{typeof(FooTableWithoutAny).FullName}' is missing field '{nameof(Foo.FooId)}'.\n" +
				$"The definition of table '{typeof(FooTableWithoutAny).FullName}' is missing field '{nameof(Foo.Bar0)}'.\n" +
				$"The definition of table '{typeof(FooTableWithoutAny).FullName}' is missing field '{nameof(Foo.Bar1)}'.",
				errors
			);
		}

		[Fact]
		public void Entity_Missing_Property_Returns_Invalid_With_Error()
		{
			// Arrange
			using var svc = new MapService();

			// Act
			var (valid, errors) = svc.ValidateTable<Foo, FooTableWithBar2>();

			// Assert
			Assert.False(valid);
			Assert.Equal(
				$"The definition of entity '{typeof(Foo).FullName}' is missing property '{nameof(FooTableWithBar2.Bar2)}'.",
				errors
			);
		}

		[Fact]
		public void Entity_Missing_Properties_Returns_Invalid_With_Errors()
		{
			// Arrange
			using var svc = new MapService();

			// Act
			var (valid, errors) = svc.ValidateTable<Foo, FooTableWithBar234>();

			// Assert
			Assert.False(valid);
			Assert.Equal(
				$"The definition of entity '{typeof(Foo).FullName}' is missing property '{nameof(FooTableWithBar234.Bar2)}'.\n" +
				$"The definition of entity '{typeof(Foo).FullName}' is missing property '{nameof(FooTableWithBar234.Bar3)}'.\n" +
				$"The definition of entity '{typeof(Foo).FullName}' is missing property '{nameof(FooTableWithBar234.Bar4)}'.",
				errors
			);
		}
	}
}
