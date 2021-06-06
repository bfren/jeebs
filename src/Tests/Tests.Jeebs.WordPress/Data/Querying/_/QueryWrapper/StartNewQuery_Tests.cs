// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.Querying.QueryWrapper_Tests
{
	public class StartNewQuery_Tests
	{
		[Fact]
		public void Returns_New_QueryBuilder_With_UnitOfWork()
		{
			// Arrange
			var unitOfWork = Substitute.For<IUnitOfWork>();
			var db = Substitute.For<IDb>();
			db.UnitOfWork.Returns(unitOfWork);
			var wrapper = new QueryWrapper(db);

			// Act
			var result = wrapper.StartNewQuery();

			// Assert
			var builder = Assert.IsType<QueryBuilder>(result);
			Assert.Equal(unitOfWork, builder.UnitOfWork);
		}
	}
}
