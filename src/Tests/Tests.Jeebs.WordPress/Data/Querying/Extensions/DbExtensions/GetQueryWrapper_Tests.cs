// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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
