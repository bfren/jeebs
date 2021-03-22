// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.Map_Tests
{
	public class To_Tests
	{
		[Fact]
		public void Without_Table_Calls_MapService_Map()
		{
			// Arrange
			var svc = Substitute.For<IMapper>();

			// Act
			Map<Foo>.To<FooTable>(svc);

			// Assert
			svc.Received().Map<Foo>(Arg.Any<FooTable>());
		}

		[Fact]
		public void With_Table_Calls_MapService_Map()
		{
			// Arrange
			var svc = Substitute.For<IMapper>();
			var table = new FooTable();

			// Act
			Map<Foo>.To(table, svc);

			// Assert
			svc.Received().Map<Foo>(Arg.Any<FooTable>());
		}
	}
}
