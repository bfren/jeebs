// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;

namespace Jeebs.Data.Clients.MySql.MySqlDbClient_Tests;

public class GetRetrieveQuery_Tests
{
	[Fact]
	public void Returns_Valid_Select_Query()
	{
		// Arrange
		var schema = Rnd.Str;
		var name = Rnd.Str;
		var table = new DbName(schema, name);

		var c0Name = Rnd.Str;
		var c0Alias = Rnd.Str;
		var c0 = new Column(table, c0Name, Helpers.CreateInfoFromAlias(c0Alias));

		var c1Name = Rnd.Str;
		var c1Alias = Rnd.Str;
		var c1 = new Column(table, c1Name, Helpers.CreateInfoFromAlias(c1Alias));

		var c2Name = Rnd.Str;
		var c2Alias = Rnd.Str;
		var c2Property = Substitute.ForPartsOf<PropertyInfo>();
		c2Property.Name.Returns(c2Alias);
		var c2 = new Column(table, c2Name, c2Property);

		var list = new ColumnList([c0, c1]);
		var client = new MySqlDbClient();

		var id = Rnd.Lng;

		var expected = "SELECT" +
			$" `{c0Name}` AS '{c0Alias}'," +
			$" `{c1Name}` AS '{c1Alias}'" +
			$" FROM `{schema}.{name}`" +
			$" WHERE `{c2Name}` = {id};";

		// Act
		var result = client.GetRetrieveQueryTest(table, list, c2, id);

		// Assert
		Assert.Equal(expected, result);
	}
}
