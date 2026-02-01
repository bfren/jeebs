// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;
using Jeebs.Data.Map;

namespace Jeebs.Data.Clients.MySql.MySqlDbClient_Tests;

public class GetDeleteQuery_Tests : MySqlDbClient_Setup
{
	[Fact]
	public void Returns_Valid_Delete_Query_Without_Version()
	{
		// Arrange
		var (client, v) = Setup();

		var schema = Rnd.Str;
		var name = Rnd.Str;
		var table = new TableName(schema, name);

		var c0Name = Rnd.Str;
		var c0Alias = Rnd.Str;
		var c0Property = Substitute.ForPartsOf<PropertyInfo>();
		c0Property.Name.Returns(c0Alias);
		var c0 = new Column(table, c0Name, c0Property);

		var id = Rnd.Lng;

		var expected = $"DELETE FROM `{schema}.{name}` WHERE `{c0Name}` = {id};";

		// Act
		var result = client.GetDeleteQueryTest(table, c0, id);

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void Returns_Valid_Delete_Query_With_Version()
	{
		// Arrange
		var (client, v) = Setup();

		var schema = Rnd.Str;
		var name = Rnd.Str;
		var table = new TableName(schema, name);

		var c0Name = Rnd.Str;
		var c0Alias = Rnd.Str;
		var c0Property = Substitute.ForPartsOf<PropertyInfo>();
		c0Property.Name.Returns(c0Alias);
		var c0 = new Column(table, c0Name, c0Property);

		var c1Name = Rnd.Str;
		var c1Alias = Rnd.Str;
		var c1Property = Substitute.ForPartsOf<PropertyInfo>();
		c1Property.Name.Returns(c1Alias);
		var c1 = new Column(table, c1Name, c1Property);

		var id = Rnd.Lng;

		var expected = $"DELETE FROM `{schema}.{name}` WHERE `{c0Name}` = {id} AND `{c1Name}` = @{c1Alias};";

		// Act
		var result = client.GetDeleteQueryTest(table, c0, id, c1);

		// Assert
		Assert.Equal(expected, result);
	}
}
