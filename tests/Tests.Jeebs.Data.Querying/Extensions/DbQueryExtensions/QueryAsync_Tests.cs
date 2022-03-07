// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Threading.Tasks;
using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.DbQueryExtensions_Tests;

public class QueryAsync_Tests
{
	[Fact]
	public async Task Calls_DbQuery_QueryAsync()
	{
		// Arrange
		var query = Substitute.For<IDbQuery>();
		var transaction = Substitute.For<IDbTransaction>();

		// Act
		_ = await query.QueryAsync<TestModel>(x => x.From<TestTable>()).ConfigureAwait(false);
		_ = await query.QueryAsync<TestModel>(x => x.From<TestTable>(), transaction).ConfigureAwait(false);

		// Assert
		_ = await query.ReceivedWithAnyArgs().QueryAsync<TestModel>(Arg.Any<IQueryParts>(), Arg.Any<IDbTransaction>()).ConfigureAwait(false);
		_ = await query.ReceivedWithAnyArgs().QueryAsync<TestModel>(Arg.Any<IQueryParts>(), transaction).ConfigureAwait(false);
	}

	public sealed record class TestTable() : Table(F.Rnd.Str)
	{
		public string Foo { get; set; } = nameof(Foo);
	}

	public sealed record class TestModel(int Foo);
}
