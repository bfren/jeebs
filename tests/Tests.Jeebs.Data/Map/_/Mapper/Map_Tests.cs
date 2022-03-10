﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.Data.Exceptions;
using Jeebs.Id;
using static Jeebs.Data.Map.Functions.MapF.M;

namespace Jeebs.Data.Map.Mapper_Tests;

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
		var action = void () => mapper.Map<Foo>(new FooTableWithoutBar0());

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
		var action = void () => mapper.Map<Foo>(new FooTableWithBar2());

		// Assert
		var ex = Assert.Throws<InvalidTableMapException>(action);
		Assert.Equal(error, ex.Message);
	}

	[Fact]
	public void Missing_Id_Property_Attribute_Throws_UnableToFindIdColumnException()
	{
		// Arrange
		using var mapper = new Mapper();
		var error = $"{typeof(NoIdPropertyMsg<FooWithoutIdAttribute>)} " +
			$"Required {nameof(IWithId.Id)} or {typeof(IdAttribute)} missing on entity {typeof(FooWithoutIdAttribute)}.";

		// Act
		var action = void () => mapper.Map<FooWithoutIdAttribute>(new FooTable());

		// Assert
		var ex = Assert.Throws<UnableToFindIdColumnException>(action);
		Assert.Equal(error, ex.Message);
	}

	[Fact]
	public void Multiple_Id_Properties_Throws_UnableToFindIdColumnException()
	{
		// Arrange
		using var svc = new Mapper();
		var error = $"{typeof(NoIdPropertyMsg<FooWithMultipleIdAttributes>)} " +
			$"Required {nameof(IWithId.Id)} or {typeof(IdAttribute)} missing on entity {typeof(FooWithMultipleIdAttributes)}.";

		// Act
		var action = void () => svc.Map<FooWithMultipleIdAttributes>(new FooTable());

		// Assert
		var ex = Assert.Throws<UnableToFindIdColumnException>(action);
		Assert.Equal(error, ex.Message);
	}

	[Fact]
	public void Missing_Version_Property_Attribute_Throws_UnableToFindVersionColumnException()
	{
		// Arrange
		using var mapper = new Mapper();
		var error = $"{typeof(NoPropertyWithAttributeMsg<FooWithoutVersionAttribute, VersionAttribute>)} " +
			$"Required {typeof(VersionAttribute)} missing on entity {typeof(FooWithoutVersionAttribute)}.";

		// Act
		var action = void () => mapper.Map<FooWithoutVersionAttribute>(new FooWithVersionTable());

		// Assert
		var ex = Assert.Throws<UnableToFindVersionColumnException>(action);
		Assert.Equal(error, ex.Message);
	}

	[Fact]
	public void Multiple_Version_Properties_Throws_UnableToFindVersionColumnException()
	{
		// Arrange
		using var svc = new Mapper();
		var error = $"{typeof(TooManyPropertiesWithAttributeMsg<FooWithMultipleVersionAttributes, VersionAttribute>)} " +
			$"More than one {typeof(VersionAttribute)} found on entity {typeof(FooWithMultipleVersionAttributes)}.";

		// Act
		var action = void () => svc.Map<FooWithMultipleVersionAttributes>(new FooWithVersionTable());

		// Assert
		var ex = Assert.Throws<UnableToFindVersionColumnException>(action);
		Assert.Equal(error, ex.Message);
	}
}