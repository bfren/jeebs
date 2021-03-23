// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.DbFunc_Tests
{
	public class UpdateAsync_Tests
	{
		[Fact]
		public async Task Calls_Client_GetUpdateQuery()
		{
			// Arrange
			var (client, crud) = DbFunc.Get();
			var value = F.Rnd.Lng;
			var model = new DbFunc.FooModel { Id = new(value) };

			// Act
			await crud.UpdateAsync(model);

			// Assert
			client.Received().GetUpdateQuery<DbFunc.Foo, DbFunc.FooModel>(value);
		}
	}
}
