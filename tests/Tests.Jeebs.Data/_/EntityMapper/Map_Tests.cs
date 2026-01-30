// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.Data.Exceptions;

namespace Jeebs.Data.EntityMapper_Tests;

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
		var table = nameof(FooTableWithoutBar0);
		var field = nameof(Foo.Bar0);

		// Act
		var result = Record.Exception(() => mapper.Map<Foo, FooTableWithoutBar0>(new()));

		// Assert
		var ex = Assert.IsType<InvalidTableMapException>(result);
		Assert.Single(ex.Errors).AssertMessage(
			"The definition of table '{Table}' is missing field '{Field}'.",
			table, field
		);
	}

	[Fact]
	public void Entity_Missing_Property_Throws_InvalidTableMapException()
	{
		// Arrange
		using var mapper = new EntityMapper();
		var entity = nameof(Foo);
		var property = nameof(FooTableWithBar2.Bar2);

		// Act
		var result = Record.Exception(() => mapper.Map<Foo, FooTableWithBar2>(new()));

		// Assert
		var ex = Assert.IsType<InvalidTableMapException>(result);
		Assert.Single(ex.Errors).AssertMessage(
			"The definition of entity '{Entity}' is missing property '{Property}'.",
			entity, property
		);
	}

	[Fact]
	public void Missing_Id_Property_Attribute_Throws_UnableToFindIdColumnException()
	{
		// Arrange
		using var mapper = new EntityMapper();
		var name = nameof(FooTableWithoutIdAttribute);

		// Act
		var result = Record.Exception(() => mapper.Map<Foo, FooTableWithoutIdAttribute>(new()));

		// Assert
		var ex = Assert.IsType<InvalidTableMapException>(result);
		Assert.Single(ex.Errors).AssertMessage("Unable to get Id column from table '{Name}': Cannot get single value from an empty list.", name);
	}

	[Fact]
	public void Multiple_Id_Properties_Throws_UnableToFindIdColumnException()
	{
		// Arrange
		using var svc = new EntityMapper();
		var name = nameof(FooTableWithMultipleIdAttributes);

		// Act
		var result = Record.Exception(() => svc.Map<Foo, FooTableWithMultipleIdAttributes>(new()));

		// Assert
		var ex = Assert.IsType<InvalidTableMapException>(result);
		Assert.Single(ex.Errors).AssertMessage(
			"Unable to get Id column from table '{Name}': Cannot get single value from a list with multiple values.",
			name
		);
	}

	[Fact]
	public void Missing_Version_Property_Attribute_Throws_UnableToFindVersionColumnException()
	{
		// Arrange
		using var mapper = new EntityMapper();
		var attribute = nameof(VersionAttribute);
		var table = nameof(FooTableWithoutVersionAttribute);

		// Act
		var result = Record.Exception(() => mapper.Map<FooWithVersion, FooTableWithoutVersionAttribute>(new()));

		// Assert
		var ex = Assert.IsType<InvalidTableMapException>(result);
		Assert.Single(ex.Errors).AssertMessage(
			"Unable to get single column with attribute '{Attribute}' from table '{Table}': Cannot get single value from an empty list.",
			attribute, table
		);
	}

	[Fact]
	public void Multiple_Version_Properties_Throws_UnableToFindVersionColumnException()
	{
		// Arrange
		using var svc = new EntityMapper();
		var attribute = nameof(VersionAttribute);
		var table = nameof(FooTableWithMultipleVersionAttributes);

		// Act
		var result = Record.Exception(() => svc.Map<FooWithVersion, FooTableWithMultipleVersionAttributes>(new()));

		// Assert
		var ex = Assert.IsType<InvalidTableMapException>(result);
		Assert.Single(ex.Errors).AssertMessage(
			"Unable to get single column with attribute '{Attribute}' from table '{Table}': Cannot get single value from a list with multiple values.",
			attribute, table
		);
	}
}
