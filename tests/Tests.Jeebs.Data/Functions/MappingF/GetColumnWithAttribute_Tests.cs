// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;
using Jeebs.Data.Entities;
using Jeebs.Data.Mapping;
using Jeebs.Data.Mapping.Mapper_Tests;
using Xunit;
using static F.DataF.MappingF;
using static F.DataF.MappingF.Msg;

namespace F.DataF.MappingF_Tests
{
	public class GetColumnWithAttribute_Tests
	{
		[Fact]
		public void Missing_Id_Property_Attribute_Returns_None_With_NoPropertyWithAttributeMsg()
		{
			// Arrange
			var columns = GetMappedColumns<FooWithoutIdAttribute>(new FooTable()).UnsafeUnwrap();

			// Act
			var result = GetColumnWithAttribute<FooWithoutIdAttribute, IdAttribute>(columns);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<NoPropertyWithAttributeMsg<FooWithoutIdAttribute, IdAttribute>>(none);
		}

		[Fact]
		public void Multiple_Id_Properties_Returns_None_With_TooManyPropertiesWithAttributeMsg()
		{
			// Arrange
			var columns = GetMappedColumns<FooWithMultipleIdAttributes>(new FooTable()).UnsafeUnwrap();

			// Act
			var result = GetColumnWithAttribute<FooWithMultipleIdAttributes, IdAttribute>(columns);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<TooManyPropertiesWithAttributeMsg<FooWithMultipleIdAttributes, IdAttribute>>(none);
		}
	}
}
