// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;
using StrongId;

namespace Jeebs.Data.Repository_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_Properties()
	{
		// Arrange
		var db = Substitute.For<IDb>();
		var log = Substitute.For<ILog<TestRepo>>();

		// Act
		var result = new TestRepo(db, log);

		// Assert
		Assert.Same(db, result.DbTest);
		Assert.Same(log, result.LogTest);
	}

	public sealed record class TestId : LongId;

	public sealed record class TestEntity(TestId Id) : IWithId<TestId>;

	public sealed class TestRepo : Repository<TestEntity, TestId>
	{
		public TestRepo(IDb db, ILog<TestRepo> log) : base(db, log) { }
	}
}
