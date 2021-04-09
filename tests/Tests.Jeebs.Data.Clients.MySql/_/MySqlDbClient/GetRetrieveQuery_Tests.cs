// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Reflection;
using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Clients.MySql.MySqlDbClient_Tests
{
	public class GetRetrieveQuery_Tests
	{
		[Fact]
		public void Returns_Valid_Select_Query()
		{
			// Arrange
			var table = F.Rnd.Str;

			var c0Name = F.Rnd.Str;
			var c0Alias = F.Rnd.Str;
			var c0 = new Column(table, c0Name, c0Alias);

			var c1Name = F.Rnd.Str;
			var c1Alias = F.Rnd.Str;
			var c1 = new Column(table, c1Name, c1Alias);

			var c2Name = F.Rnd.Str;
			var c2Alias = F.Rnd.Str;
			var c2Property = Substitute.ForPartsOf<PropertyInfo>();
			c2Property.Name.Returns(c2Alias);
			var c2 = new MappedColumn(table, c2Name, c2Property);

			var list = new ColumnList(new[] { c0, c1 });
			var client = new MySqlDbClient();

			var id = F.Rnd.Lng;

			var expected = $"SELECT `{c0Name}` AS '{c0Alias}', `{c1Name}` AS '{c1Alias}' " +
				$"FROM `{table}` WHERE `{c2Name}` = {id};";

			// Act
			var result = client.GetRetrieveQueryTest(table, list, c2, id);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}
