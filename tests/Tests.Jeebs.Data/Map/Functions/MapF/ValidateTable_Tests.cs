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
		var e0 = $"The definition of table '{typeof(FooTableWithoutBar0).FullName}' is missing field '{nameof(Foo.Bar0)}'.";

		// Act
		var (valid, errors) = MapF.ValidateTable<FooTableWithoutBar0, Foo>();

		// Assert
		Assert.False(valid);
		var single = Assert.Single(errors);
		Assert.Equal(e0, single);
	}

	[Fact]
	public void Table_Missing_Columns_Returns_Invalid_With_Errors()
	{
		// Arrange
		var e0 = $"The definition of table '{typeof(FooTableWithoutAny).FullName}' is missing field '{nameof(Foo.FooId)}'.";
		var e1 = $"The definition of table '{typeof(FooTableWithoutAny).FullName}' is missing field '{nameof(Foo.Bar0)}'.";
		var e2 = $"The definition of table '{typeof(FooTableWithoutAny).FullName}' is missing field '{nameof(Foo.Bar1)}'.";

		// Act
		var (valid, errors) = MapF.ValidateTable<FooTableWithoutAny, Foo>();

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
		var (valid, errors) = MapF.ValidateTable<FooTableWithBar2, Foo>();

		// Assert
		Assert.False(valid);
		var single = Assert.Single(errors);
		Assert.Equal(e0, single);
	}

	[Fact]
	public void Entity_Missing_Properties_Returns_Invalid_With_Errors()
	{
		// Arrange
		var e0 = $"The definition of entity '{typeof(Foo).FullName}' is missing property '{nameof(FooTableWithBar234.Bar2)}'.";
		var e1 = $"The definition of entity '{typeof(Foo).FullName}' is missing property '{nameof(FooTableWithBar234.Bar3)}'.";
		var e2 = $"The definition of entity '{typeof(Foo).FullName}' is missing property '{nameof(FooTableWithBar234.Bar4)}'.";

		// Act
		var (valid, errors) = MapF.ValidateTable<FooTableWithBar234, Foo>();

		// Assert
		Assert.False(valid);
		Assert.Collection(errors,
			x => Assert.Equal(e0, x),
			x => Assert.Equal(e1, x),
			x => Assert.Equal(e2, x)
		);
	}
}
