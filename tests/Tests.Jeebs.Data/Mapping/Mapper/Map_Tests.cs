// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Entities;
using Jeebs.Data.Exceptions;
using Xunit;

namespace Jeebs.Data.Mapping.Mapper_Tests
{
	public class Map_Tests
	{
		[Fact]
		public void Returns_Map_If_Already_Mapped()
		{
			// Arrange
			using var mapper = new Mapper();

			// Act
			var m0 = mapper.Map<Foo>(new FooTable());
			var m1 = mapper.Map<Foo>(new FooTable());

			// Assert
			Assert.Same(m0, m1);
		}

		[Fact]
		public void Table_Missing_Column_Throws_InvalidTableMapException()
		{
			// Arrange
			using var mapper = new Mapper();
			var error = $"The definition of table '{typeof(FooTableWithoutBar0)}' is missing field '{nameof(Foo.Bar0)}'.";

			// Act
			void action() => mapper.Map<Foo>(new FooTableWithoutBar0());

			// Assert
			var ex = Assert.Throws<InvalidTableMapException>(action);
			Assert.Equal(error, ex.Message);
		}

		[Fact]
		public void Entity_Missing_Property_Throws_InvalidTableMapException()
		{
			// Arrange
			using var mapper = new Mapper();
			var error = $"The definition of entity '{typeof(Foo)}' is missing property '{nameof(FooTableWithBar2.Bar2)}'.";

			// Act
			void action() => mapper.Map<Foo>(new FooTableWithBar2());

			// Assert
			var ex = Assert.Throws<InvalidTableMapException>(action);
			Assert.Equal(error, ex.Message);
		}

		[Fact]
		public void Missing_Id_Property_Attribute_Throws_UnableToFindIdColumnException()
		{
			// Arrange
			using var mapper = new Mapper();
			var error = $"Required {nameof(IWithId.Id)} or {typeof(IdAttribute)} missing on entity {typeof(FooWithoutIdAttribute)}.";

			// Act
			void action() => mapper.Map<FooWithoutIdAttribute>(new FooTable());

			// Assert
			var ex = Assert.Throws<UnableToFindIdColumnException>(action);
			Assert.Equal(error, ex.Message);
		}

		[Fact]
		public void Multiple_Id_Properties_Throws_UnableToFindIdColumnException()
		{
			// Arrange
			using var svc = new Mapper();
			var error = $"Required {nameof(IWithId.Id)} or {typeof(IdAttribute)} missing on entity {typeof(FooWithMultipleIdAttributes)}.";

			// Act
			void action() => svc.Map<FooWithMultipleIdAttributes>(new FooTable());

			// Assert
			var ex = Assert.Throws<UnableToFindIdColumnException>(action);
			Assert.Equal(error, ex.Message);
		}

		[Fact]
		public void Missing_Version_Property_Attribute_Throws_UnableToFindVersionColumnException()
		{
			// Arrange
			using var mapper = new Mapper();
			var error = $"Required {typeof(VersionAttribute)} missing on entity {typeof(FooWithoutVersionAttribute)}.";

			// Act
			void action() => mapper.Map<FooWithoutVersionAttribute>(new FooWithVersionTable());

			// Assert
			var ex = Assert.Throws<UnableToFindVersionColumnException>(action);
			Assert.Equal(error, ex.Message);
		}

		[Fact]
		public void Multiple_Version_Properties_Throws_UnableToFindVersionColumnException()
		{
			// Arrange
			using var svc = new Mapper();
			var error = $"More than one {typeof(VersionAttribute)} found on entity {typeof(FooWithMultipleVersionAttributes)}.";

			// Act
			void action() => svc.Map<FooWithMultipleVersionAttributes>(new FooWithVersionTable());

			// Assert
			var ex = Assert.Throws<UnableToFindVersionColumnException>(action);
			Assert.Equal(error, ex.Message);
		}

		//[Fact]
		//public void Creates_TableMap()
		//{
		//	// Arrange
		//	using var svc = new Mapper();
		//	var t0 = new FooTable();
		//	var t1 = new FooWithVersionTable();

		//	// Act
		//	var m0 = svc.Map<Foo, FooTable>();
		//	var m1 = svc.Map<FooWithVersion, FooWithVersionTable>();

		//	// Assert
		//	Assert.Equal(t0.ToString(), m0.Name);
		//	Assert.Equal(t0.FooId, m0.IdColumn.Name);
		//	Assert.Equal(nameof(t0.FooId), m0.IdColumn.Alias);

		//	Assert.Equal(t1.ToString(), m1.Name);
		//	Assert.Equal(t1.FooId, m1.IdColumn.Name);
		//	Assert.Equal(nameof(t1.FooId), m1.IdColumn.Alias);
		//	Assert.Equal(t1.Version, m1.VersionColumn?.Name);
		//	Assert.Equal(nameof(t1.Version), m1.VersionColumn?.Alias);
		//}
	}
}
