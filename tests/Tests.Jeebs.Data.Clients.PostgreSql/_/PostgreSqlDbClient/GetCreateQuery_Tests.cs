// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;
using Jeebs.Data.Map;
using NSubstitute.Extensions;

namespace Jeebs.Data.Clients.PostgreSql.PostgreSqlDbClient_Tests;

public class GetCreateQuery_Tests
{
	[Fact]
	public void Returns_Valid_Insert_Query()
	{
		// Arrange
		var schema = Rnd.Str;
		var name = Rnd.Str;
		var table = new DbName(schema, name);

		var c0Name = Rnd.Str;
		var c0Alias = Rnd.Str;
		var c0Property = Substitute.ForPartsOf<PropertyInfo>();
		c0Property.Name.Returns(c0Alias);
		c0Property.Configure().CustomAttributes.Returns(Array.Empty<CustomAttributeData>());
		var c0 = new Column(table, c0Name, c0Property);

		var c1Name = Rnd.Str;
		var c1Alias = Rnd.Str;
		var c1Property = Substitute.ForPartsOf<PropertyInfo>();
		c1Property.Name.Returns(c1Alias);
		c1Property.Configure().CustomAttributes.Returns(Array.Empty<CustomAttributeData>());
		var c1 = new Column(table, c1Name, c1Property);

		var list = new ColumnList(new[] { c0, c1 });
		var client = new PostgreSqlDbClient();

		var expected = $"INSERT INTO \"{schema}\".\"{name}\" (\"{c0Name}\", \"{c1Name}\") VALUES (@{c0Alias}, @{c1Alias}); " +
			"SELECT LASTVAL();";

		// Act
		var result = client.GetCreateQueryTest(table, list);

		// Assert
		Assert.Equal(expected, result);
	}
}
