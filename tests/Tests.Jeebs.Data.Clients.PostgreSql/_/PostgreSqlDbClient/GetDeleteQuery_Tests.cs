// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Reflection;
using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Clients.PostgreSql.PostgreSqlDbClient_Tests;

public class GetDeleteQuery_Tests
{
	[Fact]
	public void Returns_Valid_Delete_Query_Without_Version()
	{
		// Arrange
		var schema = F.Rnd.Str;
		var table = F.Rnd.Str;

		var c0Name = F.Rnd.Str;
		var c0Alias = F.Rnd.Str;
		var c0Property = Substitute.ForPartsOf<PropertyInfo>();
		c0Property.Name.Returns(c0Alias);
		var c0 = new MappedColumn($"{schema}.{table}", c0Name, c0Property);

		var client = new PostgreSqlDbClient();

		var id = F.Rnd.Lng;

		var expected = $"DELETE FROM \"{schema}\".\"{table}\" WHERE \"{c0Name}\" = {id};";

		// Act
		var result = client.GetDeleteQueryTest($"{schema}.{table}", c0, id);

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void Returns_Valid_Delete_Query_With_Version()
	{
		// Arrange
		var schema = F.Rnd.Str;
		var table = F.Rnd.Str;

		var c0Name = F.Rnd.Str;
		var c0Alias = F.Rnd.Str;
		var c0Property = Substitute.ForPartsOf<PropertyInfo>();
		c0Property.Name.Returns(c0Alias);
		var c0 = new MappedColumn($"{schema}.{table}", c0Name, c0Property);

		var c1Name = F.Rnd.Str;
		var c1Alias = F.Rnd.Str;
		var c1Property = Substitute.ForPartsOf<PropertyInfo>();
		c1Property.Name.Returns(c1Alias);
		var c1 = new MappedColumn($"{schema}.{table}", c1Name, c1Property);

		var client = new PostgreSqlDbClient();

		var id = F.Rnd.Lng;

		var expected = $"DELETE FROM \"{schema}\".\"{table}\" WHERE \"{c0Name}\" = {id} AND \"{c1Name}\" = @{c1Alias};";

		// Act
		var result = client.GetDeleteQueryTest($"{schema}.{table}", c0, id, c1);

		// Assert
		Assert.Equal(expected, result);
	}
}
