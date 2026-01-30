// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.EntityMapper_Tests;

public class Dispose_Tests
{
	[Fact]
	public void Clears_Mapped_Entities()
	{
		// Arrange
		var svc = new EntityMapper();
		svc.Map<Foo, FooTable>(new());

		// Act
		svc.Dispose();

		// Assert
		_ = svc.GetTableMapFor<Foo>().AssertFailure();
	}
}
