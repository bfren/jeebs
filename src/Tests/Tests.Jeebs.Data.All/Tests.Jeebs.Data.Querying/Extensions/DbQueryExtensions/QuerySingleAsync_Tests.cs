﻿// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Threading.Tasks;
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

			// Act
			await query.QuerySingleAsync<TestModel>(x => x.From<TestTable>());

			// Assert
			await query.ReceivedWithAnyArgs().QuerySingleAsync<TestModel>(Arg.Any<IQueryParts>(), null);
		}

		public sealed record TestTable() : Table(F.Rnd.Str)
		{
			public string Foo { get; set; } = nameof(Foo);
		}

		public sealed record TestModel(int Foo);
	}
}
