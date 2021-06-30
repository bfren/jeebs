// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using NSubstitute;
using Xunit;

namespace Jeebs.Data.Repository_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Properties()
		{
			// Arrange
			var db = Substitute.For<IDb>();
			var log = Substitute.For<ILog>();

			// Act
			var result = new TestFunc(db, log);

			// Assert
			Assert.Same(db, result.DbTest);
			Assert.Same(log, result.LogTest);
		}

		public sealed record TestId(ulong Value) : StrongId(Value);

		public sealed record TestEntity(TestId Id) : IWithId<TestId>;

		public sealed class TestFunc : Repository<TestEntity, TestId>
		{
			public TestFunc(IDb db, ILog log) : base(db, log) { }
		}
	}
}
