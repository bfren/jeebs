// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
