// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace Jeebs.Data.Common.Query.FluentQuery_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Entity_Is_Not_Mapped__Adds_Reason_To_Errors()
	{
		// Arrange
		var db = Substitute.For<IDb>();
		var reason = FailGen.Create();
		var mapper = Substitute.For<IEntityMapper>();
		mapper.GetTableMapFor<TestEntity>()
			.Returns(reason);
		var log = Substitute.For<ILog>();

		// Act
		var result = new FluentQuery<TestEntity, TestId>(db, mapper, log);

		// Assert
		var single = Assert.Single(result.Errors);
		Assert.Equal(reason.Value, single);
	}

	[Fact]
	public void Entity_Is_Not_Mapped__Uses_NullTable_For_Parts()
	{
		// Arrange
		var db = Substitute.For<IDb>();
		var reason = FailGen.Create();
		var mapper = Substitute.For<IEntityMapper>();
		mapper.GetTableMapFor<TestEntity>()
			.Returns(reason);
		var log = Substitute.For<ILog>();

		// Act
		var result = new FluentQuery<TestEntity, TestId>(db, mapper, log);

		// Assert
		Assert.IsType<NullTable>(result.Parts.From);
	}

	[Fact]
	public void Entity_Is_Mapped__Uses_Table_For_Parts()
	{
		// Arrange
		var db = Substitute.For<IDb>();
		var map = Substitute.For<ITableMap>();
		var table = Substitute.For<ITable>();
		map.Table
			.Returns(table);
		var mapper = Substitute.For<IEntityMapper>();
		mapper.GetTableMapFor<TestEntity>()
			.Returns(R.Wrap(map));
		var log = Substitute.For<ILog>();

		// Act
		var result = new FluentQuery<TestEntity, TestId>(db, mapper, log);

		// Assert
		Assert.Empty(result.Errors);
		Assert.Same(table, result.Parts.From);
	}

	public sealed record class TestId : GuidId<TestId>;

	public sealed record class TestEntity : WithId<TestId, Guid>;
}
