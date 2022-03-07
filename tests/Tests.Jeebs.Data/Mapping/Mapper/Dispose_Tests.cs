// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Data.Mapping.Mapper_Tests;

public class Dispose_Tests
{
	[Fact]
	public void Clears_Mapped_Entities()
	{
		// Arrange
		var svc = new Mapper();
		_ = svc.Map<Foo>(new FooTable());

		// Act
		svc.Dispose();

		// Assert
		_ = svc.GetTableMapFor<Foo>().AssertNone();
	}
}
