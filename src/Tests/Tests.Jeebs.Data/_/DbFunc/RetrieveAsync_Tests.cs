// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.DbFunc_Tests
{
	public class RetrieveAsync_Tests
	{
		[Fact]
		public async Task Calls_Client_GetRetrieveQuery()
		{
			// Arrange
			var (client, crud) = DbFunc.Get();
			var value = F.Rnd.Lng;

			// Act
			await crud.RetrieveAsync<DbFunc.FooModel>(new DbFunc.FooId(value));

			// Assert
			client.Received().GetRetrieveQuery<DbFunc.Foo, DbFunc.FooModel>(value);
		}
	}
}
