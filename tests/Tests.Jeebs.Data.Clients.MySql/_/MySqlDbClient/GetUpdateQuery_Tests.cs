// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;
using Jeebs.Data.Map;

namespace Jeebs.Data.Clients.MySql.MySqlDbClient_Tests;

public class GetUpdateQuery_Tests
{
	[Fact]
	public void Returns_Valid_Update_Query_Without_Version()
	{
		// Arrange
		var schema = Rnd.Str;
		var name = Rnd.Str;
		var table = new DbName(schema, name);

		var c0Name = Rnd.Str;
		var c0Alias = Rnd.Str;
		var c0PropertyInfo = Substitute.For<PropertyInfo>();
		c0PropertyInfo.Name.Returns(c0Alias);
		var c0 = new Column(table, c0Name, c0PropertyInfo);

		var c1Name = Rnd.Str;
		var c1Alias = Rnd.Str;
		var c1PropertyInfo = Substitute.For<PropertyInfo>();
		c1PropertyInfo.Name.Returns(c1Alias);
		var c1 = new Column(table, c1Name, c1PropertyInfo);

		var c2Name = Rnd.Str;
		var c2Alias = Rnd.Str;
		var c2Property = Substitute.ForPartsOf<PropertyInfo>();
		c2Property.Name.Returns(c2Alias);
		var c2 = new Column(table, c2Name, c2Property);

		var list = new ColumnList(new[] { c0, c1 });
		var client = new MySqlDbClient();

		var id = Rnd.Lng;

		var expected = $"UPDATE `{schema}.{name}` SET" +
			$" `{c0Name}` = @{c0Alias}," +
			$" `{c1Name}` = @{c1Alias}" +
			$" WHERE `{c2Name}` = {id};";

		// Act
		var result = client.GetUpdateQueryTest(table, list, c2, id);

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void Returns_Valid_Update_Query_With_Version()
	{
		// Arrange
		var schema = Rnd.Str;
		var name = Rnd.Str;
		var table = new DbName(schema, name);

		var c0Name = Rnd.Str;
		var c0Alias = Rnd.Str;
		var c0PropertyInfo = Substitute.For<PropertyInfo>();
		c0PropertyInfo.Name.Returns(c0Alias);
		var c0 = new Column(table, c0Name, c0PropertyInfo);

		var c1Name = Rnd.Str;
		var c1Alias = Rnd.Str;
		var c1PropertyInfo = Substitute.For<PropertyInfo>();
		c1PropertyInfo.Name.Returns(c1Alias);
		var c1 = new Column(table, c1Name, c1PropertyInfo);

		var c2Name = Rnd.Str;
		var c2Alias = Rnd.Str;
		var c2Property = Substitute.ForPartsOf<PropertyInfo>();
		c2Property.Name.Returns(c2Alias);
		var c2 = new Column(table, c2Name, c2Property);

		var c3Name = Rnd.Str;
		var c3Alias = Rnd.Str;
		var c3Property = Substitute.ForPartsOf<PropertyInfo>();
		c3Property.Name.Returns(c3Alias);
		var c3 = new Column(table, c3Name, c3Property);

		var list = new ColumnList(new[] { c0, c1 });
		var client = new MySqlDbClient();

		var id = Rnd.Lng;

		var expected = $"UPDATE `{schema}.{name}` SET" +
			$" `{c0Name}` = @{c0Alias}," +
			$" `{c1Name}` = @{c1Alias}," +
			$" `{c3Name}` = @{c3Alias} + 1" +
			$" WHERE `{c2Name}` = {id}" +
			$" AND `{c3Name}` = @{c3Alias};";

		// Act
		var result = client.GetUpdateQueryTest(table, list, c2, id, c3);

		// Assert
		Assert.Equal(expected, result);
	}
}
