// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace Jeebs.Data.Repository.Repository_Tests;

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

	public sealed record class TestId : LongId<TestId>;

	public sealed record class TestEntity : WithId<TestId, long>;

	public sealed class TestRepo(IDb db, ILog<TestRepo> log) : Repository<TestEntity, TestId>(db, log);
}
