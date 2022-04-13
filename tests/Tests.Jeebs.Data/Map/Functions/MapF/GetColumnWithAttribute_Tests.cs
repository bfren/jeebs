// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Attributes;
using Jeebs.Data.Map._.Mapper.Tables;
using Jeebs.Data.Map.Mapper_Tests;
using static Jeebs.Data.Map.Functions.MapF.M;

namespace Jeebs.Data.Map.Functions.MapF_Tests;

public class GetColumnWithAttribute_Tests
{
	[Fact]
	public void Missing_Id_Property_Attribute_Returns_None_With_NoPropertyWithAttributeMsg()
	{
		// Arrange
		var columns = MapF.GetColumns<FooTableWithoutIdAttribute, Foo>(new()).UnsafeUnwrap();

		// Act
		var result = MapF.GetColumnWithAttribute<FooTableWithoutIdAttribute, IdAttribute>(columns);

		// Assert
		result.AssertNone().AssertType<NoPropertyWithAttributeMsg<FooTableWithoutIdAttribute, IdAttribute>>();
	}

	[Fact]
	public void Multiple_Id_Properties_Returns_None_With_TooManyPropertiesWithAttributeMsg()
	{
		// Arrange
		var columns = MapF.GetColumns<FooTableWithMultipleIdAttributes, Foo>(new()).UnsafeUnwrap();

		// Act
		var result = MapF.GetColumnWithAttribute<FooTableWithMultipleIdAttributes, IdAttribute>(columns);

		// Assert
		result.AssertNone().AssertType<TooManyPropertiesWithAttributeMsg<FooTableWithMultipleIdAttributes, IdAttribute>>();
	}
}
