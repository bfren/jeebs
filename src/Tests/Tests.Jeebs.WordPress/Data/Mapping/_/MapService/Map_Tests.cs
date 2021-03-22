// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jx.Data.Mapping;
using Xunit;

namespace Jeebs.Data.Mapping.Mapper_Tests
{
	public class Map_Tests
	{
		[Fact]
		public void Returns_Map_If_Already_Mapped()
		{
			// Arrange
			using var svc = new MapService();

			// Act
			var m0 = svc.Map<Foo, FooTable>();
			var m1 = svc.Map<Foo, FooTable>();

			// Assert
			Assert.StrictEqual(m0, m1);
		}

		[Fact]
		public void Table_Missing_Column_Throws_InvalidTableMapException()
		{
			// Arrange
			using var svc = new MapService();

			// Act
			void action() => svc.Map<Foo, FooTableWithoutBar0>();

			// Assert
			var ex = Assert.Throws<InvalidTableMapException>(action);
			Assert.Equal($"The definition of table '{typeof(FooTableWithoutBar0).FullName}' is missing field 'Bar0'.", ex.Message);
		}

		[Fact]
		public void Entity_Missing_Property_Throws_InvalidTableMapException()
		{
			// Arrange
			using var svc = new MapService();

			// Act
			void action() => svc.Map<Foo, FooTableWithBar2>();

			// Assert
			var ex = Assert.Throws<InvalidTableMapException>(action);
			Assert.Equal($"The definition of entity '{typeof(Foo).FullName}' is missing property 'Bar2'.", ex.Message);
		}

		[Fact]
		public void Missing_Id_Property_Attribute_Throws_MissingAttributeException()
		{
			// Arrange
			using var svc = new MapService();

			// Act
			void action() => svc.Map<FooWithoutIdAttribute, FooTable>();

			// Assert
			var ex = Assert.Throws<MissingAttributeException>(action);
			Assert.Equal(string.Format(MissingAttributeException.Format, "Id", typeof(FooWithoutIdAttribute)), ex.Message);
		}

		[Fact]
		public void Multiple_Id_Properties_Throws_MultipleAttributesException()
		{
			// Arrange
			using var svc = new MapService();

			// Act
			void action() => svc.Map<FooWithMultipleIdAttributes, FooTable>();

			// Assert
			var ex = Assert.Throws<MultipleAttributesException>(action);
			Assert.Equal(string.Format(MultipleAttributesException.Format, "Id", typeof(FooWithMultipleIdAttributes)), ex.Message);
		}

		[Fact]
		public void Missing_Version_Property_Attribute_Throws_MissingAttributeException()
		{
			// Arrange
			using var svc = new MapService();

			// Act
			void action() => svc.Map<FooWithoutVersionAttribute, FooWithVersionTable>();

			// Assert
			var ex = Assert.Throws<MissingAttributeException>(action);
			Assert.Equal(string.Format(MissingAttributeException.Format, "Version", typeof(FooWithoutVersionAttribute)), ex.Message);
		}

		[Fact]
		public void Multiple_Version_Properties_Throws_MultipleAttributesException()
		{
			// Arrange
			using var svc = new MapService();

			// Act
			void action() => svc.Map<FooWithMultipleVersionAttributes, FooWithVersionTable>();

			// Assert
			var ex = Assert.Throws<MultipleAttributesException>(action);
			Assert.Equal(string.Format(MultipleAttributesException.Format, "Version", typeof(FooWithMultipleVersionAttributes)), ex.Message);
		}

		[Fact]
		public void Creates_TableMap()
		{
			// Arrange
			using var svc = new MapService();
			var t0 = new FooTable();
			var t1 = new FooWithVersionTable();

			// Act
			var m0 = svc.Map<Foo, FooTable>();
			var m1 = svc.Map<FooWithVersion, FooWithVersionTable>();

			// Assert
			Assert.Equal(t0.ToString(), m0.Name);
			Assert.Equal(t0.FooId, m0.IdColumn.Name);
			Assert.Equal(nameof(t0.FooId), m0.IdColumn.Alias);

			Assert.Equal(t1.ToString(), m1.Name);
			Assert.Equal(t1.FooId, m1.IdColumn.Name);
			Assert.Equal(nameof(t1.FooId), m1.IdColumn.Alias);
			Assert.Equal(t1.Version, m1.VersionColumn?.Name);
			Assert.Equal(nameof(t1.Version), m1.VersionColumn?.Alias);
		}
	}
}
