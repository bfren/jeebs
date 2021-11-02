// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Reflection;
using Jeebs.Data.Mapping;
using NSubstitute;
using NSubstitute.Extensions;
using Xunit;

namespace Jeebs.Data.Clients.SqlServer.SqlServerDbClient_Tests;

public class GetCreateQuery_Tests
{
	[Fact]
	public void Returns_Valid_Insert_Query()
	{
		// Arrange
		var schema = F.Rnd.Str;
		var name = F.Rnd.Str;
		var table = new TableName(schema, name);

		var c0Name = F.Rnd.Str;
		var c0Alias = F.Rnd.Str;
		var c0Property = Substitute.ForPartsOf<PropertyInfo>();
		c0Property.Name.Returns(c0Alias);
		c0Property.Configure().CustomAttributes.Returns(Array.Empty<CustomAttributeData>());
		var c0 = new MappedColumn(table, c0Name, c0Property);

		var c1Name = F.Rnd.Str;
		var c1Alias = F.Rnd.Str;
		var c1Property = Substitute.ForPartsOf<PropertyInfo>();
		c1Property.Name.Returns(c1Alias);
		c1Property.Configure().CustomAttributes.Returns(Array.Empty<CustomAttributeData>());
		var c1 = new MappedColumn(table, c1Name, c1Property);

		var list = new MappedColumnList(new[] { c0, c1 });
		var client = new SqlServerDbClient();

		var expected = $"INSERT INTO [{schema}].[{name}] ([{c0Name}], [{c1Name}]) VALUES (@{c0Alias}, @{c1Alias}); " +
			"SELECT SCOPE_IDENTITY();";

		// Act
		var result = client.GetCreateQueryTest(table, list);

		// Assert
		Assert.Equal(expected, result);
	}
}
