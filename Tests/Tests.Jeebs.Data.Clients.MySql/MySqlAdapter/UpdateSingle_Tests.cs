using System;
using Xunit;

namespace Jeebs.Data.Clients.MySql
{
	public partial class MySqlAdapter_Tests
	{
		[Fact]
		public void UpdateSingle()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			Map<Foo>.To(new FooTable(), adapter);
			Map<FooWithVersion>.To(new FooWithVersionTable(), adapter);
			const string expected = "UPDATE `foo` SET `foo_bar0` = @Bar0, `foo_bar1` = @Bar1 WHERE `foo_id` = @Id;";
			const string expected_with_version = "UPDATE `foo_with_version` SET `foo_bar0` = @Bar0, `foo_bar1` = @Bar1, `foo_version` = @Version " +
				"WHERE `foo_id` = @Id AND `foo_version` = @Version - 1;";

			// Act
			var result = adapter.UpdateSingle<Foo>();
			var result_with_version = adapter.UpdateSingle<FooWithVersion>();

			// Assert
			Assert.Equal(expected, result);
			Assert.Equal(expected_with_version, result_with_version);
		}
	}
}
