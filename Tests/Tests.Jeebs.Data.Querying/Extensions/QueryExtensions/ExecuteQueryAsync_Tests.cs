using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryExtensions_Tests
{
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
	public class ExecuteQueryAsync_Tests
	{
		[Fact]
		public async Task Calls_ExecuteQueryAsync_With_Result_Ok()
		{
			// Arrange
			var query = Substitute.For<IQuery<int>>();

			// Act
			query.ExecuteQueryAsync();

			// Assert
			query.Received().ExecuteQueryAsync(Arg.Any<IOk>());
		}

		[Fact]
		public async Task Calls_ExecuteQueryAsync_With_Result_Ok_And_Page()
		{
			// Arrange
			var query = Substitute.For<IQuery<int>>();
			var page = F.Rnd.Lng;

			// Act
			query.ExecuteQueryAsync(page);

			// Assert
			query.Received().ExecuteQueryAsync(Arg.Any<IOk>(), page);
		}
	}
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
}
