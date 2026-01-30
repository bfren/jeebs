// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.Common.DbClient_Tests;

public class AddVersionToWhere_Tests
{
	[Fact]
	public void VersionColumn_Null_Does_Nothing()
	{
		// Arrange
		var client = Substitute.ForPartsOf<DbClient>();
		var sql = string.Empty;

		// Act
		client.AddVersionToWhereTest(sql, null);

		// Assert
		Assert.Equal(string.Empty, sql);
	}

	[Fact]
	public void Adds_Version_To_Where()
	{
		// Arrange
		var client = Substitute.ForPartsOf<DbClient>();
		client.Escape(Arg.Any<IColumn>()).Returns(x => $"--{x.ArgAt<IColumn>(0).ColName}--");
		client.GetParamRef(Arg.Any<string>()).Returns(x => $"##{x.ArgAt<string>(0)}##");

		var name = Rnd.Str;
		var alias = Rnd.Str;
		var version = new Column(new TableName(Rnd.Str), name, Helpers.CreateInfoFromAlias(alias));
		var expected = $"--{name}-- = ##{alias}##";

		var sql = string.Empty;

		// Act
		var result = client.AddVersionToWhereTest(sql, version);

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void Adds_And_Version_To_Where()
	{
		// Arrange
		var client = Substitute.ForPartsOf<DbClient>();
		client.Escape(Arg.Any<IColumn>()).Returns(x => $"--{x.ArgAt<IColumn>(0).ColName}--");
		client.GetParamRef(Arg.Any<string>()).Returns(x => $"##{x.ArgAt<string>(0)}##");

		var name = Rnd.Str;
		var alias = Rnd.Str;
		var version = new Column(new TableName(Rnd.Str), name, Helpers.CreateInfoFromAlias(alias));

		var query = Rnd.Str;
		var sql = query;

		var expected = $"{query} AND --{name}-- = ##{alias}##";

		// Act
		var result = client.AddVersionToWhereTest(sql, version);

		// Assert
		Assert.Equal(expected, result);
	}
}
