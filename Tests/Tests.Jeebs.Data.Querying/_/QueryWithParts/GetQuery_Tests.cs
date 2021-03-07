// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryWithParts_Tests
{
	public class GetQuery_Tests
	{
		[Fact]
		public void Returns_New_Query_With_UnitOfWork_And_Parts()
		{
			// Arrange
			var unitOfWork = Substitute.For<IUnitOfWork>();
			var parts = Substitute.For<IQueryParts>();
			var withParts = new QueryBuilder<string>.QueryWithParts(unitOfWork, parts);

			// Act
			var result = withParts.GetQuery();

			// Assert
			var query = Assert.IsType<Query<string>>(result);
			Assert.Equal(unitOfWork, query.UnitOfWork);
			Assert.Equal(parts, query.Parts);
		}
	}
}
