using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryWithOptions_Tests
{
	public class WithParts_Tests
	{
		[Fact]
		public void Returns_New_QueryWithParts_With_UnitOfWork_And_Parts()
		{
			// Arrange
			var unitOfWork = Substitute.For<IUnitOfWork>();
			var options = Substitute.For<QueryOptions>();
			var withOptions = new QueryBuilder<string>.QueryWithOptions<QueryOptions>(unitOfWork, options);
			var parts = Substitute.For<IQueryParts>();

			// Act
			var result = withOptions.WithParts(parts);

			// Assert
			var withParts = Assert.IsType<QueryBuilder<string>.QueryWithParts>(result);
			Assert.Equal(unitOfWork, withParts.UnitOfWork);
			Assert.Equal(parts, withParts.Parts);
		}

		[Fact]
		public void Calls_QueryPartsBuilder_Build()
		{
			// Arrange
			var unitOfWork = Substitute.For<IUnitOfWork>();
			var options = Substitute.For<QueryOptions>();
			var withOptions = new QueryBuilder<string>.QueryWithOptions<QueryOptions>(unitOfWork, options);
			var parts = Substitute.For<IQueryParts>();
			var partsBuilder = Substitute.For<IQueryPartsBuilder<string, QueryOptions>>();
			partsBuilder.Build(options).Returns(parts);

			// Act
			var result = withOptions.WithParts(partsBuilder);

			// Assert
			partsBuilder.Received().Build(options);
			var withParts = Assert.IsType<QueryBuilder<string>.QueryWithParts>(result);
			Assert.Equal(unitOfWork, withParts.UnitOfWork);
			Assert.Equal(parts, withParts.Parts);
		}
	}
}
