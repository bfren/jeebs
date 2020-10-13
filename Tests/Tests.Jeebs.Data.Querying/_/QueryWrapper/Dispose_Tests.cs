using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryWrapper_Tests
{
	public class Dispose_Tests
	{
		[Fact]
		public void Calls_UnitOfWork_Dispose()
		{
			// Arrange
			var unitOfWork = Substitute.For<IUnitOfWork>();
			var db = Substitute.For<IDb>();
			db.UnitOfWork.Returns(unitOfWork);
			var wrapper = new QueryWrapper(db);

			// Act
			wrapper.Dispose();

			// Assert
			unitOfWork.Received().Dispose();
		}
	}
}
