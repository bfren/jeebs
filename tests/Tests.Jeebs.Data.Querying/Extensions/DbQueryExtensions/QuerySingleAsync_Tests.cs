// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Threading.Tasks;
using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.DbQueryExtensions_Tests;

public class QuerySingleAsync_Tests
{
	[Fact]
	public async Task Calls_DbQuery_QuerySingleAsync()
	{
		// Arrange
		var query = Substitute.For<IDbQuery>();
		var transaction = Substitute.For<IDbTransaction>();

		// Act
		await query.QuerySingleAsync<TestModel>(x => x.From<TestTable>()).ConfigureAwait(false);
		await query.QuerySingleAsync<TestModel>(x => x.From<TestTable>(), transaction).ConfigureAwait(false);

		// Assert
		await query.ReceivedWithAnyArgs().QuerySingleAsync<TestModel>(Arg.Any<IQueryParts>(), Arg.Any<IDbTransaction>()).ConfigureAwait(false);
		await query.ReceivedWithAnyArgs().QuerySingleAsync<TestModel>(Arg.Any<IQueryParts>(), transaction).ConfigureAwait(false);
	}

	public sealed record class TestTable() : Table(F.Rnd.Str)
	{
		public string Foo { get; set; } = nameof(Foo);
	}

	public sealed record class TestModel(int Foo);
}
