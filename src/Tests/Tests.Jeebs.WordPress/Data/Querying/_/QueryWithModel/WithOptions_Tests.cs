// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryWithModel_Tests
{
	public class WithOptions_Tests
	{
		[Fact]
		public void Returns_New_QueryWithOptions_With_UnitOfWork_And_Options()
		{
			// Arrange
			var unitOfWork = Substitute.For<IUnitOfWork>();
			var withModel = new QueryBuilder<string>.QueryWithModel(unitOfWork);
			var options = Substitute.For<QueryOptions>();

			// Act
			var result = withModel.WithOptions(options);

			// Assert
			var withOptions = Assert.IsType<QueryBuilder<string>.QueryWithOptions<QueryOptions>>(result);
			Assert.Equal(unitOfWork, withOptions.UnitOfWork);
			Assert.Equal(options, withOptions.Options);
		}

		[Fact]
		public void Invokes_Modify_Returns_New_QueryWithOptions_With_UnitOfWork()
		{
			// Arrange
			var unitOfWork = Substitute.For<IUnitOfWork>();
			var withModel = new QueryBuilder<string>.QueryWithModel(unitOfWork);
			var modify = Substitute.For<Action<Options>>();

			// Act
			var result = withModel.WithOptions(modify);

			// Assert
			modify.Received().Invoke(Arg.Any<Options>());
			var withOptions = Assert.IsType<QueryBuilder<string>.QueryWithOptions<Options>>(result);
			Assert.Equal(unitOfWork, withOptions.UnitOfWork);
		}

		public class Options : QueryOptions { }
	}
}
