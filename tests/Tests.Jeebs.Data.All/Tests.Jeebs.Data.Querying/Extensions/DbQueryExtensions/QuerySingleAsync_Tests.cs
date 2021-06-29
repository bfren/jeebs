﻿// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Data;
using System.Threading.Tasks;
using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.DbQueryExtensions_Tests
{
	public class QuerySingleAsync_Tests
	{
		[Fact]
		public async Task Calls_DbQuery_QuerySingleAsync()
		{
			// Arrange
			var query = Substitute.For<IDbQuery>();
			var transaction = Substitute.For<IDbTransaction>();

			// Act
			await query.QuerySingleAsync<TestModel>(x => x.From<TestTable>());
			await query.QuerySingleAsync<TestModel>(x => x.From<TestTable>(), transaction);

			// Assert
			await query.ReceivedWithAnyArgs().QuerySingleAsync<TestModel>(Arg.Any<IQueryParts>(), Arg.Any<IDbTransaction>());
			await query.ReceivedWithAnyArgs().QuerySingleAsync<TestModel>(Arg.Any<IQueryParts>(), transaction);
		}

		public sealed record TestTable() : Table(F.Rnd.Str)
		{
			public string Foo { get; set; } = nameof(Foo);
		}

		public sealed record TestModel(int Foo);
	}
}
