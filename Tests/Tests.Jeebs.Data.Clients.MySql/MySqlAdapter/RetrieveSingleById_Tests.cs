using System;
using Xunit;

namespace Jeebs.Data.Clients.MySql
{
	public partial class MySqlAdapter_Tests
	{
		[Fact]
		public void RetrieveSingleById()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			Map<Foo>.To(new FooTable(), adapter);
			const string expected = "SELECT `foo_id` AS 'Id', `foo_bar0` AS 'Bar0', `foo_bar1` AS 'Bar1' FROM `foo` WHERE `foo_id` = @Id;";

			// Act
			var result = adapter.RetrieveSingleById<Foo>();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
