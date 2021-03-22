// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.DbCrud_Tests
{
	public class RetrieveAsync_Tests
	{
		[Fact]
		public async Task Calls_Client_GetRetrieveQuery()
		{
			// Arrange
			var (client, crud) = DbCrud.Get();

			// Act
			await crud.RetrieveAsync<DbCrud.FooModel>(new DbCrud.FooId(F.Rnd.Int));

			// Assert
			client.Received().GetRetrieveQuery<DbCrud.Foo, DbCrud.FooModel>();
		}
	}
}
