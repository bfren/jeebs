// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Map.Mapper_Tests;

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
