// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;
using Jeebs.Data.Map;
using NSubstitute.Extensions;

namespace Jeebs.Data.Clients.MySql.MySqlDbClient_Tests;

public class GetCreateQuery_Tests
{
	[Fact]
	public void Returns_Valid_Insert_Query()
	{
		// Arrange
		var schema = Rnd.Str;
		var name = Rnd.Str;
		var table = new TableName(schema, name);

		var c0Name = Rnd.Str;
		var c0Alias = Rnd.Str;
		var c0Property = Substitute.ForPartsOf<PropertyInfo>();
		_ = c0Property.Name.Returns(c0Alias);
		_ = c0Property.Configure().CustomAttributes.Returns(Array.Empty<CustomAttributeData>());
		var c0 = new MappedColumn(table, c0Name, c0Property);

		var c1Name = Rnd.Str;
		var c1Alias = Rnd.Str;
		var c1Property = Substitute.ForPartsOf<PropertyInfo>();
		_ = c1Property.Name.Returns(c1Alias);
		_ = c1Property.Configure().CustomAttributes.Returns(Array.Empty<CustomAttributeData>());
		var c1 = new MappedColumn(table, c1Name, c1Property);

		var list = new MappedColumnList(new[] { c0, c1 });
		var client = new MySqlDbClient();

		var expected = $"INSERT INTO `{schema}.{name}` (`{c0Name}`, `{c1Name}`) VALUES (@{c0Alias}, @{c1Alias}); " +
			"SELECT LAST_INSERT_ID();";

		// Act
		var result = client.GetCreateQueryTest(table, list);

		// Assert
		Assert.Equal(expected, result);
	}
}
