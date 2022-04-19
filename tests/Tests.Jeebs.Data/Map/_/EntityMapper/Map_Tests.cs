// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.Data.Exceptions;
using Jeebs.Data.Map._.Mapper.Tables;
using StrongId;
using static Jeebs.Data.Map.Functions.MapF.M;

namespace Jeebs.Data.Map.EntityMapper_Tests;

public class Map_Tests
{
	[Fact]
	public void Returns_Map_If_Already_Mapped()
	{
		// Arrange
		using var mapper = new EntityMapper();

		// Act
		var m0 = mapper.Map<Foo, FooTable>(new());
		var m1 = mapper.Map<Foo, FooTable>(new());

		// Assert
		Assert.Same(m0, m1);
	}

	[Fact]
	public void Table_Missing_Column_Throws_InvalidTableMapException()
	{
		// Arrange
		using var mapper = new EntityMapper();
		var error = $"The definition of table '{typeof(FooTableWithoutBar0)}' is missing field '{nameof(Foo.Bar0)}'.";

		// Act
		var action = void () => mapper.Map<Foo, FooTableWithoutBar0>(new());

		// Assert
		var ex = Assert.Throws<InvalidTableMapException>(action);
		Assert.Equal(error, ex.Message);
	}

	[Fact]
	public void Entity_Missing_Property_Throws_InvalidTableMapException()
	{
		// Arrange
		using var mapper = new EntityMapper();
		var error = $"The definition of entity '{typeof(Foo)}' is missing property '{nameof(FooTableWithBar2.Bar2)}'.";

		// Act
		var action = void () => mapper.Map<Foo, FooTableWithBar2>(new());

		// Assert
		var ex = Assert.Throws<InvalidTableMapException>(action);
		Assert.Equal(error, ex.Message);
	}

	[Fact]
	public void Missing_Id_Property_Attribute_Throws_UnableToFindIdColumnException()
	{
		// Arrange
		using var mapper = new EntityMapper();
		var error = $"{typeof(NoIdPropertyMsg<FooTableWithoutIdAttribute>)} " +
			$"Required {nameof(IWithId.Id)} or {typeof(IdAttribute)} missing on table {typeof(FooTableWithoutIdAttribute)}.";

		// Act
		var action = void () => mapper.Map<Foo, FooTableWithoutIdAttribute>(new());

		// Assert
		var ex = Assert.Throws<UnableToFindIdColumnException>(action);
		Assert.Equal(error, ex.Message);
	}

	[Fact]
	public void Multiple_Id_Properties_Throws_UnableToFindIdColumnException()
	{
		// Arrange
		using var svc = new EntityMapper();
		var error = $"{typeof(NoIdPropertyMsg<FooTableWithMultipleIdAttributes>)} " +
			$"Required {nameof(IWithId.Id)} or {typeof(IdAttribute)} missing on table {typeof(FooTableWithMultipleIdAttributes)}.";

		// Act
		var action = void () => svc.Map<Foo, FooTableWithMultipleIdAttributes>(new());

		// Assert
		var ex = Assert.Throws<UnableToFindIdColumnException>(action);
		Assert.Equal(error, ex.Message);
	}

	[Fact]
	public void Missing_Version_Property_Attribute_Throws_UnableToFindVersionColumnException()
	{
		// Arrange
		using var mapper = new EntityMapper();
		var error = $"{typeof(NoPropertyWithAttributeMsg<FooTableWithoutVersionAttribute, VersionAttribute>)} " +
			$"Required {typeof(VersionAttribute)} missing on table {typeof(FooTableWithoutVersionAttribute)}.";

		// Act
		var action = void () => mapper.Map<FooWithVersion, FooTableWithoutVersionAttribute>(new());

		// Assert
		var ex = Assert.Throws<UnableToFindVersionColumnException>(action);
		Assert.Equal(error, ex.Message);
	}

	[Fact]
	public void Multiple_Version_Properties_Throws_UnableToFindVersionColumnException()
	{
		// Arrange
		using var svc = new EntityMapper();
		var error = $"{typeof(TooManyPropertiesWithAttributeMsg<FooTableWithMultipleVersionAttributes, VersionAttribute>)} " +
			$"More than one {typeof(VersionAttribute)} found on table {typeof(FooTableWithMultipleVersionAttributes)}.";

		// Act
		var action = void () => svc.Map<FooWithVersion, FooTableWithMultipleVersionAttributes>(new());

		// Assert
		var ex = Assert.Throws<UnableToFindVersionColumnException>(action);
		Assert.Equal(error, ex.Message);
	}
}
