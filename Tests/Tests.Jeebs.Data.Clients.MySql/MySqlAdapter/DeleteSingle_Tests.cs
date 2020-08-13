using System;
using Xunit;

namespace Jeebs.Data.Clients.MySql
{
	public partial class MySqlAdapter_Tests
	{
		[Fact]
		public void DeleteSingle()
		{
			// Arrange
			var adapter = new MySqlAdapter();
			Map<Foo>.To(new FooTable(), adapter);
			Map<FooWithVersion>.To(new FooWithVersionTable(), adapter);
			const string expected = "DELETE FROM `foo` WHERE `foo_id` = @Id;";
			const string expected_with_version = "DELETE FROM `foo_with_version` WHERE `foo_id` = @Id AND `foo_version` = @Version - 1;";

			// Act
			var result = adapter.DeleteSingle<Foo>();
			var result_with_version = adapter.DeleteSingle<FooWithVersion>();

			// Assert
			Assert.Equal(expected, result);
			Assert.Equal(expected_with_version, result_with_version);
		}
	}
}
