// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.DbFunc_Tests
{
	public class CreateAsync_Tests
	{
		[Fact]
		public async Task Calls_Client_GetCreateQuery()
		{
			// Arrange
			var (client, crud) = DbFunc.Get();
			var entity = new DbFunc.Foo { FooId = new(F.Rnd.Int) };

			// Act
			await crud.CreateAsync(entity);

			// Assert
			client.Received().GetCreateQuery<DbFunc.Foo>();
		}
	}
}
