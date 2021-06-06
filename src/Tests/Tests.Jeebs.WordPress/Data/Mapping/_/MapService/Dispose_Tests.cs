// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jx.Data.Mapping;
using Xunit;

namespace Jeebs.WordPress.Data.Mapping.Mapper_Tests
{
	public class Dispose_Tests
	{
		[Fact]
		public void Clears_Mapped_Entities()
		{
			// Arrange
			var svc = new MapService();
			svc.Map<Foo, FooTable>();

			void getMap() => svc.GetTableMapFor<Foo>();

			// Act
			svc.Dispose();

			// Assert
			Assert.Throws<UnmappedEntityException>(getMap);
		}
	}
}
