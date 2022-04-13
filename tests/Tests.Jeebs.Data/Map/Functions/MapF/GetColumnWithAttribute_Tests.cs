// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.Data.Map.Mapper_Tests;
using static Jeebs.Data.Map.Functions.MapF.M;

namespace Jeebs.Data.Map.Functions.MapF_Tests;

public class GetColumnWithAttribute_Tests
{
	[Fact]
	public void Missing_Id_Property_Attribute_Returns_None_With_NoPropertyWithAttributeMsg()
	{
		// Arrange
		var columns = MapF.GetColumns<FooWithoutIdAttribute>(new FooTable()).UnsafeUnwrap();

		// Act
		var result = MapF.GetColumnWithAttribute<FooWithoutIdAttribute, IdAttribute>(columns);

		// Assert
		result.AssertNone().AssertType<NoPropertyWithAttributeMsg<FooWithoutIdAttribute, IdAttribute>>();
	}

	[Fact]
	public void Multiple_Id_Properties_Returns_None_With_TooManyPropertiesWithAttributeMsg()
	{
		// Arrange
		var columns = MapF.GetColumns<FooWithMultipleIdAttributes>(new FooTable()).UnsafeUnwrap();

		// Act
		var result = MapF.GetColumnWithAttribute<FooWithMultipleIdAttributes, IdAttribute>(columns);

		// Assert
		result.AssertNone().AssertType<TooManyPropertiesWithAttributeMsg<FooWithMultipleIdAttributes, IdAttribute>>();
	}
}
