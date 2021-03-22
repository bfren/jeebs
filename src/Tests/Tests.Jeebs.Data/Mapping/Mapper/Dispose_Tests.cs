// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Data.Mapping.Mapper_Tests
{
	public class Dispose_Tests
	{
		[Fact]
		public void Clears_Mapped_Entities()
		{
			// Arrange
			var svc = new Mapper();
			svc.Map<Foo>(new FooTable());

			// Act
			svc.Dispose();

			// Assert
			svc.GetTableMapFor<Foo>().AssertNone();
		}
	}
}
