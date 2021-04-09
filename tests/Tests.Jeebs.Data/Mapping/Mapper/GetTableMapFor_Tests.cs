// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static Jeebs.Data.Mapping.Mapper.Msg;

namespace Jeebs.Data.Mapping.Mapper_Tests
{
	public class GetTableMapFor_Tests
	{
		[Fact]
		public void Unmapped_Entity_Returns_None_With_TryingToGetUnmappedEntityMsg()
		{
			// Arrange
			using var mapper = new Mapper();

			// Act
			var result = mapper.GetTableMapFor<Foo>();

			// Assert
			var none = result.AssertNone();
			Assert.IsType<TryingToGetUnmappedEntityMsg<Foo>>(none);
		}

		[Fact]
		public void Mapped_Entity_Returns_Some_With_TableMap()
		{
			// Arrange
			using var mapper = new Mapper();
			var map = mapper.Map<Foo>(new FooTable());

			// Act
			var result = mapper.GetTableMapFor<Foo>();

			// Assert
			var some = result.AssertSome();
			Assert.Same(map, some);
		}
	}
}
