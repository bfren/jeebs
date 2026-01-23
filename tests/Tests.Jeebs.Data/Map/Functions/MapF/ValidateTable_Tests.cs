// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map.EntityMapper_Tests;

namespace Jeebs.Data.Map.Functions.MapF_Tests;

public class ValidateTable_Tests
{
	[Fact]
	public void Table_Missing_Column_Returns_Invalid_With_Error()
	{
		// Arrange
		var table = nameof(FooTableWithoutBar0);
		var field = nameof(Foo.Bar0);

		// Act
		var (valid, errors) = MapF.ValidateTable<FooTableWithoutBar0, Foo>();

		// Assert
		Assert.False(valid);
		Assert.Single(errors)
			.AssertMessage(
				"The definition of table '{Table}' is missing field '{Field}'.",
				new { table, field }
			);
	}

	[Fact]
	public void Table_Missing_Columns_Returns_Invalid_With_Errors()
	{
		// Arrange
		var message = "The definition of table '{Table}' is missing field '{Field}'.";
		var table = nameof(FooTableWithoutAny);
		var f0 = nameof(Foo.FooId);
		var f1 = nameof(Foo.Bar0);
		var f2 = nameof(Foo.Bar1);

		// Act
		var (valid, errors) = MapF.ValidateTable<FooTableWithoutAny, Foo>();

		// Assert
		Assert.False(valid);
		Assert.Collection(errors,
			x => x.AssertMessage(message, new { table, field = f0 }),
			x => x.AssertMessage(message, new { table, field = f1 }),
			x => x.AssertMessage(message, new { table, field = f2 })
		);
	}

	[Fact]
	public void Entity_Missing_Property_Returns_Invalid_With_Error()
	{
		// Arrange

		// Act
		var (valid, errors) = MapF.ValidateTable<FooTableWithBar2, Foo>();

		// Assert
		Assert.False(valid);
		var single = Assert.Single(errors);
		Assert.Single(errors)
			.AssertMessage(
				"The definition of entity '{Entity}' is missing property '{Property}'.",
				new { Entity = nameof(Foo), Property = nameof(FooTableWithBar2.Bar2) }
			);
	}

	[Fact]
	public void Entity_Missing_Properties_Returns_Invalid_With_Errors()
	{
		// Arrange
		var message = "The definition of entity '{Entity}' is missing property '{Property}'.";
		var entity = nameof(Foo);
		var p0 = nameof(FooTableWithBar234.Bar2);
		var p1 = nameof(FooTableWithBar234.Bar3);
		var p2 = nameof(FooTableWithBar234.Bar4);

		// Act
		var (valid, errors) = MapF.ValidateTable<FooTableWithBar234, Foo>();

		// Assert
		Assert.False(valid);
		Assert.Collection(errors,
			x => x.AssertMessage(message, new { entity, property = p0 }),
			x => x.AssertMessage(message, new { entity, property = p1 }),
			x => x.AssertMessage(message, new { entity, property = p2 })
		);
	}
}
