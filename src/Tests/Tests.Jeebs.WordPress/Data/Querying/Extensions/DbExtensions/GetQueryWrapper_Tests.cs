// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.Querying.DbExtensions_Tests
{
	public class GetQueryWrapper_Tests
	{
		[Fact]
		public void Returns_QueryWrapper()
		{
			// Arrange
			var db = Substitute.For<IDb>();

			// Act
			var result = db.GetQueryWrapper();

			// Assert
			var _ = db.Received().UnitOfWork;
			Assert.IsType<QueryWrapper>(result);
		}
	}
}
