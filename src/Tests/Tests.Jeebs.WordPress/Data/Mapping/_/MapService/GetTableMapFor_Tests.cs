// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jx.Data.Mapping;
using Xunit;

namespace Jeebs.WordPress.Data.Mapping.Mapper_Tests
{
	public class GetTableMapFor_Tests
	{
		[Fact]
		public void Unmapped_Entity_Throws_UnmappedEntityException()
		{
			// Arrange
			using var svc = new MapService();

			// Act
			void action() => svc.GetTableMapFor<Foo>();

			// Assert
			Assert.Throws<UnmappedEntityException>(action);
		}

		[Fact]
		public void Mapped_Entity_Returns_TableMap()
		{
			// Arrange
			using var svc = new MapService();
			var m0 = svc.Map<Foo, FooTable>();

			// Act
			var m1 = svc.GetTableMapFor<Foo>();

			// Assert
			Assert.StrictEqual(m0, m1);
		}
	}
}
