// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Id;
using Jeebs.Logging;

namespace Jeebs.Data.Repository_Tests;

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

	public sealed record class TestId : StrongId;

	public sealed record class TestEntity(TestId Id) : IWithId<TestId>;

	public sealed class TestFunc : Repository<TestEntity, TestId>
	{
		public TestFunc(IDb db, ILog log) : base(db, log) { }
	}
}
