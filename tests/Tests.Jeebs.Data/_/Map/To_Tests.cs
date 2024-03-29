// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.Map_Tests;

public class To_Tests
{
	[Fact]
	public void Without_Table_Calls_MapService_Map()
	{
		// Arrange
		var svc = Substitute.For<IEntityMapper>();

		// Act
		Map<Foo>.To<FooTable>(svc);

		// Assert
		svc.Received().Map<Foo, FooTable>(Arg.Any<FooTable>());
	}

	[Fact]
	public void With_Table_Calls_MapService_Map()
	{
		// Arrange
		var svc = Substitute.For<IEntityMapper>();
		var table = new FooTable();

		// Act
		Map<Foo>.To(table, svc);

		// Assert
		svc.Received().Map<Foo, FooTable>(Arg.Any<FooTable>());
	}
}
