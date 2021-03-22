// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static Jeebs.Data.Mapper.Msg;

namespace Jeebs.Data.Mapping.Mapper_Tests
{
	public class GetColumnWithAttribute_Tests
	{
		[Fact]
		public void Missing_Id_Property_Attribute_Returns_None_With_NoPropertyWithAttributeMsg()
		{
			// Arrange
			var columns = Mapper.GetMappedColumns<FooWithoutIdAttribute>(new FooTable()).UnsafeUnwrap();

			// Act
			var result = Mapper.GetColumnWithAttribute<FooWithoutIdAttribute, IdAttribute>(columns);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<NoPropertyWithAttributeMsg<FooWithoutIdAttribute, IdAttribute>>(none);
		}

		[Fact]
		public void Multiple_Id_Properties_Returns_None_With_TooManyPropertiesWithAttributeMsg()
		{
			// Arrange
			var columns = Mapper.GetMappedColumns<FooWithMultipleIdAttributes>(new FooTable()).UnsafeUnwrap();

			// Act
			var result = Mapper.GetColumnWithAttribute<FooWithMultipleIdAttributes, IdAttribute>(columns);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<TooManyPropertiesWithAttributeMsg<FooWithMultipleIdAttributes, IdAttribute>>(none);
		}
	}
}
