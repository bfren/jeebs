// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.DbClient_Tests;

public class AddVersionToSetList_Tests : DbClient_Setup
{
	[Fact]
	public void VersionColumn_Null_Does_Nothing()
	{
		// Arrange
		var (client, _) = Setup();
		var set = new List<string>();

		// Act
		client.AddVersionToSetListTest(set, null);

		// Assert
		Assert.Empty(set);
	}

	[Fact]
	public void Adds_VersionColumn_To_Columns()
	{
		// Arrange
		var (client, _) = Setup();

		var name = Rnd.Str;
		var alias = Rnd.Str;
		var version = new Column(new TableName(Rnd.Str), name, Helpers.CreateInfoFromAlias(alias));
		var expected = $"--{name}-- = ##{alias}## + 1";

		var set = new List<string>();

		// Act
		client.AddVersionToSetListTest(set, version);

		// Assert
		var single = Assert.Single(set);
		Assert.Equal(expected, single);
	}
}
