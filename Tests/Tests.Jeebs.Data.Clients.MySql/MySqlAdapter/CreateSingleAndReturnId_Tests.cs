using System;
using Xunit;

namespace Jeebs.Data.Clients.MySql
{
	public partial class MySqlAdapter_Tests
	{
		[Fact]
		public void CreateSingleAndReturnId()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			Map<Foo>.To(new FooTable(), adapter);
			const string expected = "INSERT INTO `foo` (`foo_bar0`, `foo_bar1`) VALUES (@Bar0, @Bar1); SELECT LAST_INSERT_ID();";

			// Act
			var result = adapter.CreateSingleAndReturnId<Foo>();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
