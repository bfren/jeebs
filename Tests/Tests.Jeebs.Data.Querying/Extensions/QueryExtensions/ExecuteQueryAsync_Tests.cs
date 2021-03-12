// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryExtensions_Tests
{
	public class ExecuteQueryAsync_Tests
	{
		[Fact]
		public async Task Calls_ExecuteQueryAsync()
		{
			// Arrange
			var query = Substitute.For<IQuery<int>>();

			// Act
			await query.ExecuteQueryAsync();

			// Assert
			await query.Received().ExecuteQueryAsync();
		}

		[Fact]
		public async Task Calls_ExecuteQueryAsync_With_Result_Ok_And_Page()
		{
			// Arrange
			var query = Substitute.For<IQuery<int>>();
			var page = F.Rnd.Lng;

			// Act
			await query.ExecuteQueryAsync(page);

			// Assert
			await query.Received().ExecuteQueryAsync(page);
		}
	}
}
