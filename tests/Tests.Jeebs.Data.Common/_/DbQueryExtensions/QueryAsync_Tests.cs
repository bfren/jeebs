// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.Query.DbQueryExtensions_Tests;

public class QueryAsync_Tests
{
	[Fact]
	public async Task Calls_DbQuery_QueryAsync()
	{
		// Arrange
		var query = Substitute.For<IDbQuery>();
		var transaction = Substitute.For<IDbTransaction>();

		// Act
		await query.QueryAsync<TestModel>(x => x.From<TestTable>());
		await query.QueryAsync<TestModel>(x => x.From<TestTable>(), transaction);

		// Assert
		await query.ReceivedWithAnyArgs().QueryAsync<TestModel>(Arg.Any<IQueryParts>(), Arg.Any<IDbTransaction>());
		await query.ReceivedWithAnyArgs().QueryAsync<TestModel>(Arg.Any<IQueryParts>(), transaction);
	}

	public sealed record class TestTable() : Table(Rnd.Str)
	{
		public string Foo { get; set; } = nameof(Foo);
	}

	public sealed record class TestModel(int Foo);
}
