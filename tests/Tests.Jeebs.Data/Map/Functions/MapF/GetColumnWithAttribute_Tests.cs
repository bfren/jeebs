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
		var columns = MapF.GetMappedColumns<FooWithoutIdAttribute>(new FooTable()).UnsafeUnwrap();

		// Act
		var result = MapF.GetColumnWithAttribute<FooWithoutIdAttribute, IdAttribute>(columns);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<NoPropertyWithAttributeMsg<FooWithoutIdAttribute, IdAttribute>>(none);
	}

	[Fact]
	public void Multiple_Id_Properties_Returns_None_With_TooManyPropertiesWithAttributeMsg()
	{
		// Arrange
		var columns = MapF.GetMappedColumns<FooWithMultipleIdAttributes>(new FooTable()).UnsafeUnwrap();

		// Act
		var result = MapF.GetColumnWithAttribute<FooWithMultipleIdAttributes, IdAttribute>(columns);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<TooManyPropertiesWithAttributeMsg<FooWithMultipleIdAttributes, IdAttribute>>(none);
	}
}
