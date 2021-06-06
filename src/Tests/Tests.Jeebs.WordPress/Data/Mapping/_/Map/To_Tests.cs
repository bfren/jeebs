// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.Mapping.Map_Tests
{
	public class To_Tests
	{
		[Fact]
		public void Without_Table_Calls_MapService_Map()
		{
			// Arrange
			var svc = Substitute.For<IMapService>();

			// Act
			Map<Foo>.To<FooTable>(svc);

			// Assert
			svc.Received().Map<Foo>(Arg.Any<FooTable>());
		}

		[Fact]
		public void With_Table_Calls_MapService_Map()
		{
			// Arrange
			var svc = Substitute.For<IMapService>();
			var table = new FooTable();

			// Act
			Map<Foo>.To(table, svc);

			// Assert
			svc.Received().Map<Foo>(Arg.Any<FooTable>());
		}
	}
}
