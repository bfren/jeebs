// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryExtensions_Tests
{
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
	public class GetCountAsync_Tests
	{
		[Fact]
		public async Task Calls_GetCountAsync_With_Result_Ok()
		{
			// Arrange
			var query = Substitute.For<IQuery<int>>();

			// Act
			query.GetCountAsync();

			// Assert
			query.Received().GetCountAsync(Arg.Any<IOk>());
		}
	}
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
}
