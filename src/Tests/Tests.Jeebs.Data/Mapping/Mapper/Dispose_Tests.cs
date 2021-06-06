// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
