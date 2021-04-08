// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.Querying.QueryWrapper_Tests
{
	public class Dispose_Tests
	{
		[Fact]
		public void Calls_UnitOfWork_Dispose()
		{
			// Arrange
			var unitOfWork = Substitute.For<IUnitOfWork>();
			var db = Substitute.For<IDb>();
			db.UnitOfWork.Returns(unitOfWork);
			var wrapper = new QueryWrapper(db);

			// Act
			wrapper.Dispose();

			// Assert
			unitOfWork.Received().Dispose();
		}
	}
}
