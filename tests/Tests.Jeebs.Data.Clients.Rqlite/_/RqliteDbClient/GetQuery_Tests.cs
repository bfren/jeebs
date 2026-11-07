// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.Query;
using Jeebs.Functions;

namespace Jeebs.Data.Clients.Rqlite.RqliteDbClient_Tests;

public class GetQuery_Tests
{
	private static (RqliteDbClient client, ITable table, string name) Setup()
	{
		var name = Rnd.Str;
		var table = Substitute.For<ITable>();
		table.GetName().Returns(new TableName(Rnd.Str, name));
		return (new(), table, name);
	}

	[Fact]
	public void SortRandom_Adds_Random_Without_Terminator()
	{
		// Arrange
		var (client, table, name) = Setup();
		var parts = new QueryParts(table) { SortRandom = true };

		// Act
		var (query, _) = client.GetQuery(parts).Unsafe().Unwrap();

		// Assert
		Assert.Equal($"SELECT * FROM {name} ORDER BY random()", query);
	}

	[Fact]
	public void Maximum_And_Skip_Adds_Limit_With_Offset()
	{
		// Arrange
		var (client, table, name) = Setup();
		var (max, skip) = (Rnd.UInt64, Rnd.UInt64);
		var parts = new QueryParts(table) { Maximum = max, Skip = skip };

		// Act
		var (query, _) = client.GetQuery(parts).Unsafe().Unwrap();

		// Assert
		Assert.Equal($"SELECT * FROM {name} LIMIT {skip}, {max}", query);
	}

	[Fact]
	public void Maximum_Only_Adds_Limit()
	{
		// Arrange
		var (client, table, name) = Setup();
		var max = Rnd.UInt64;
		var parts = new QueryParts(table) { Maximum = max };

		// Act
		var (query, _) = client.GetQuery(parts).Unsafe().Unwrap();

		// Assert
		Assert.Equal($"SELECT * FROM {name} LIMIT {max}", query);
	}

	[Fact]
	public void Skip_Only_Adds_Offset_With_No_Limit()
	{
		// Arrange
		var (client, table, name) = Setup();
		var skip = Rnd.UInt64;
		var parts = new QueryParts(table) { Skip = skip };

		// Act
		var (query, _) = client.GetQuery(parts).Unsafe().Unwrap();

		// Assert
		Assert.Equal($"SELECT * FROM {name} LIMIT {skip}, -1", query);
	}

	[Fact]
	public void GetCountQuery_Removes_Sort_And_Paging()
	{
		// Arrange
		var (client, table, name) = Setup();
		IColumn sort = new Column(new TableName(Rnd.Str, Rnd.Str), Rnd.Str, Helpers.CreateInfoFromAlias());
		var parts = new QueryParts(table)
		{
			Sort = ListF.Create([(sort, SortOrder.Descending)]),
			SortRandom = true,
			Maximum = Rnd.UInt64,
			Skip = Rnd.UInt64
		};

		// Act
		var (query, _) = client.GetCountQuery(parts).Unsafe().Unwrap();

		// Assert
		Assert.Equal($"SELECT COUNT(*) FROM {name}", query);
	}
}
