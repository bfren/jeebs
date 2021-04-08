// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.Querying.QueryExtensions_Tests
{
	public class GetCountAsync_Tests
	{
		[Fact]
		public async Task Calls_GetCountAsync_With_Result_Ok()
		{
			// Arrange
			var query = Substitute.For<IQuery<int>>();

			// Act
			await query.GetCountAsync();

			// Assert
			await query.Received().GetCountAsync();
		}
	}
}
