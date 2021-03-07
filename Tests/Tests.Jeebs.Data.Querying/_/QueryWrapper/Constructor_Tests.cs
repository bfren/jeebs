// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryWrapper_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_UnitOfWork()
		{
			// Arrange
			var unitOfWork = Substitute.For<IUnitOfWork>();
			var db = Substitute.For<IDb>();
			db.UnitOfWork.Returns(unitOfWork);

			// Act
			var result = new QueryWrapper(db);

			// Assert
			Assert.Equal(unitOfWork, result.UnitOfWork);
		}
	}
}
