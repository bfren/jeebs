using Xunit;

namespace Jeebs.Data.Clients.MySql.MySqlAdapter_Tests
{
	public class GetRandomSortOrder_Tests
	{
		[Fact]
		public void Returns_Rand()
		{
			// Arrange
			var adapter = new MySqlAdapter();

			// Act
			var result = adapter.GetRandomSortOrder();

			// Assert
			Assert.Equal("RAND()", result);
		}
	}
}
