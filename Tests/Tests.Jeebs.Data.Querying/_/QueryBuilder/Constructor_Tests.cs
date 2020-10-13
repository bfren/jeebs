using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryBuilder_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_UnitOfWork()
		{
			// Arrange
			var unitOfWork = Substitute.For<IUnitOfWork>();

			// Act
			var result = new QueryBuilder(unitOfWork);

			// Assert
			Assert.Equal(unitOfWork, result.UnitOfWork);
		}
	}
}
