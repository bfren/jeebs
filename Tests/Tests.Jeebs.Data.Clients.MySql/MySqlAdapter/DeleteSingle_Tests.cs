using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Data.Clients.MySql.MySqlAdapter_Tests
{
	public class DeleteSingle_Tests
	{
		[Fact]
		public void Returns_Delete_Query()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			Map<Foo>.To(new FooTable(), adapter);
			const string expected = "DELETE FROM `foo` WHERE `foo_id` = @Id;";

			// Act
			var result = adapter.DeleteSingle<Foo>();

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Returns_Delete_Query_With_Version()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			Map<FooWithVersion>.To(new FooWithVersionTable(), adapter);
			const string expected = "DELETE FROM `foo_with_version` WHERE `foo_id` = @Id AND `foo_version` = @Version - 1;";

			// Act
			var result = adapter.DeleteSingle<FooWithVersion>();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
