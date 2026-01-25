// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.Data.Map.EntityMapper_Tests;
using Jeebs.Data.Map.Mapper.Tables;

namespace Jeebs.Data.Map.Functions.MapF_Tests;

public class GetColumnWithAttribute_Tests
{
	[Fact]
	public void Missing_Id_Property_Attribute_Returns_None_With_NoPropertyWithAttributeMsg()
	{
		// Arrange
		var columns = MapF.GetColumns<FooTableWithoutIdAttribute, Foo>(new()).Unsafe().Unwrap();

		// Act
		var result = MapF.GetColumnWithAttribute<FooTableWithoutIdAttribute, IdAttribute>(columns);

		// Assert
		_ = result.AssertFail(
			"Unable to get single column with attribute '{Attribute}' from table '{Table}': Cannot get single value from an empty list.",
			"IdAttribute", "FooTableWithoutIdAttribute"
		);
	}

	[Fact]
	public void Multiple_Id_Properties_Returns_None_With_TooManyPropertiesWithAttributeMsg()
	{
		// Arrange
		var columns = MapF.GetColumns<FooTableWithMultipleIdAttributes, Foo>(new()).Unsafe().Unwrap();

		// Act
		var result = MapF.GetColumnWithAttribute<FooTableWithMultipleIdAttributes, IdAttribute>(columns);

		// Assert
		_ = result.AssertFail(
			"Unable to get single column with attribute '{Attribute}' from table '{Table}': Cannot get single value from a list with multiple values.",
			"IdAttribute", "FooTableWithMultipleIdAttributes"
		);
	}
}
