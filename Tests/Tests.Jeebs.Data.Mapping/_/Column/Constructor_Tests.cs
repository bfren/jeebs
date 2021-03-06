using Xunit;

namespace Jeebs.Data.Mapping.Column_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Properties_Set()
		{
			// Arrange
			var table = F.Rnd.Str;
			var name = F.Rnd.Str;
			var alias = F.Rnd.Str;
			var column = new Column(table, name, alias);

			// Act

			// Assert
			Assert.Equal(table, column.Table);
			Assert.Equal(name, column.Name);
			Assert.Equal(alias, column.Alias);
		}
	}
}
