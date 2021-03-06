using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryWithModel_Tests
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
