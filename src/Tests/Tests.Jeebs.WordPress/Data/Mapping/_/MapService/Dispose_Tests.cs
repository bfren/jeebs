// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jx.Data.Mapping;
using Xunit;

namespace Jeebs.Data.Mapping.Mapper_Tests
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
