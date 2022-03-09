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
		var svc = Substitute.For<IMapper>();

		// Act
		_ = Map<Foo>.To<FooTable>(svc);

		// Assert
		_ = svc.Received().Map<Foo>(Arg.Any<FooTable>());
	}

	[Fact]
	public void With_Table_Calls_MapService_Map()
	{
		// Arrange
		var svc = Substitute.For<IMapper>();
		var table = new FooTable();

		// Act
		_ = Map<Foo>.To(table, svc);

		// Assert
		_ = svc.Received().Map<Foo>(Arg.Any<FooTable>());
	}
}
