using Xunit;

namespace Jeebs.Data.Clients.SqlServer.SqlServerAdapter_Tests
{
	public class GetSelectCount_Tests
	{
		[Fact]
		public void Returns_Select_Count()
		{
			// Arrange
			var adapter = new SqlServerAdapter();

			// Act
			var result = adapter.GetSelectCount();

			// Assert
			Assert.Equal("COUNT(*)", result);
		}
	}
}
