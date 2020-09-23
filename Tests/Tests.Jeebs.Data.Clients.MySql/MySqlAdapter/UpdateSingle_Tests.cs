using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Data.Clients.MySql.MySqlAdapter_Tests
{
	public class UpdateSingle_Tests
	{
		[Fact]
		public void Returns_Update_Query()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			Map<Foo>.To(new FooTable(), adapter);
			const string expected = "UPDATE `foo` SET `foo_bar0` = @Bar0, `foo_bar1` = @Bar1 WHERE `foo_id` = @Id;";

			// Act
			var result = adapter.UpdateSingle<Foo>();

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Returns_Update_Query_With_Version()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			Map<FooWithVersion>.To(new FooWithVersionTable(), adapter);
			const string expected = "UPDATE `foo_with_version` SET `foo_bar0` = @Bar0, `foo_bar1` = @Bar1, `foo_version` = @Version " +
				"WHERE `foo_id` = @Id AND `foo_version` = @Version - 1;";

			// Act
			var result = adapter.UpdateSingle<FooWithVersion>();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
