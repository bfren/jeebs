// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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

		public sealed record TestId(long Value) : StrongId(Value);

		public sealed record TestEntity(TestId Id) : IEntity<TestId>;

		public sealed class TestFunc : Repository<TestEntity, TestId>
		{
			public TestFunc(IDb db, ILog log) : base(db, log) { }
		}
	}
}
