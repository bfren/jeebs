// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Reflection;
using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Clients.SqlServer.SqlServerDbClient_Tests
{
	public class GetDeleteQuery_Tests
	{
		[Fact]
		public void Returns_Valid_Delete_Query_Without_Version()
		{
			// Arrange
			var table = F.Rnd.Str;

			var c0Name = F.Rnd.Str;
			var c0Alias = F.Rnd.Str;
			var c0Property = Substitute.ForPartsOf<PropertyInfo>();
			c0Property.Name.Returns(c0Alias);
			var c0 = new MappedColumn(table, c0Name, c0Property);

			var client = new SqlServerDbClient();

			var id = F.Rnd.Lng;

			var expected = $"DELETE FROM [{table}] WHERE [{c0Name}] = {id}";

			// Act
			var result = client.GetDeleteQueryTest(table, c0, id);

			// Assert
			Assert.Equal(expected, result);
		}

		[Fact]
		public void Returns_Valid_Delete_Query_With_Version()
		{
			// Arrange
			var table = F.Rnd.Str;

			var c0Name = F.Rnd.Str;
			var c0Alias = F.Rnd.Str;
			var c0Property = Substitute.ForPartsOf<PropertyInfo>();
			c0Property.Name.Returns(c0Alias);
			var c0 = new MappedColumn(table, c0Name, c0Property);

			var c1Name = F.Rnd.Str;
			var c1Alias = F.Rnd.Str;
			var c1Property = Substitute.ForPartsOf<PropertyInfo>();
			c1Property.Name.Returns(c1Alias);
			var c1 = new MappedColumn(table, c1Name, c1Property);

			var client = new SqlServerDbClient();

			var id = F.Rnd.Lng;

			var expected = $"DELETE FROM [{table}] WHERE [{c0Name}] = {id} AND [{c1Name}] = @{c1Alias}";

			// Act
			var result = client.GetDeleteQueryTest(table, c0, id, c1);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
