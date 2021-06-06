// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.Querying.QueryWithModel_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_UnitOfWork()
		{
			// Arrange
			var unitOfWork = Substitute.For<IUnitOfWork>();

			// Act
			var result = new QueryBuilder<string>.QueryWithModel(unitOfWork);

			// Assert
			Assert.Equal(unitOfWork, result.UnitOfWork);
		}
	}
}
