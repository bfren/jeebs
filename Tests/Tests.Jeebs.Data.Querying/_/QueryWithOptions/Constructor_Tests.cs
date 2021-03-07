// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryWithOptions_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_UnitOfWork_And_Options()
		{
			// Arrange
			var unitOfWork = Substitute.For<IUnitOfWork>();
			var options = Substitute.For<QueryOptions>();

			// Act
			var result = new QueryBuilder<string>.QueryWithOptions<QueryOptions>(unitOfWork, options);

			// Assert
			Assert.Equal(unitOfWork, result.UnitOfWork);
			Assert.Equal(options, result.Options);
		}
	}
}
