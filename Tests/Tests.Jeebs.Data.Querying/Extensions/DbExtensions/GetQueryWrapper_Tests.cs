using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.DbExtensions_Tests
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
