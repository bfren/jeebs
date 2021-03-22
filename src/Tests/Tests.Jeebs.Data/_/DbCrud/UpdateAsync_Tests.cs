// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.DbCrud_Tests
{
	public class UpdateAsync_Tests
	{
		[Fact]
		public async Task Calls_Client_GetUpdateQuery()
		{
			// Arrange
			var (client, crud) = DbCrud.Get();
			var model = new DbCrud.FooModel { FooId = new(F.Rnd.Int) };

			// Act
			await crud.UpdateAsync(model);

			// Assert
			client.Received().GetUpdateQuery<DbCrud.Foo, DbCrud.FooModel>();
		}
	}
}
