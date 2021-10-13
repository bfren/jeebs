// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using Xunit;

namespace Jeebs.Data.Clients.MySql.MySqlDbClient_Tests;

public class GetQuery_Tests
{
	[Fact]
	public void With_Predicates_Returns_Valid_Select_Query()
	{
		// Arrange
		var table = F.Rnd.Str;

		var c0Name = F.Rnd.Str;
		var c0Alias = F.Rnd.Str;
		var c0 = new Column(table, c0Name, c0Alias);

		var c1Name = F.Rnd.Str;
		var c1Alias = F.Rnd.Str;
		var c1 = new Column(table, c1Name, c1Alias);

		var list = new ColumnList(new[] { c0, c1 });

		var p0Column = new Column(table, F.Rnd.Str, F.Rnd.Str);
		var p0Operator = Compare.Like;
		var p0Value = F.Rnd.Str;

		var p1Column = new Column(table, F.Rnd.Str, F.Rnd.Str);
		var p1Operator = Compare.MoreThanOrEqual;
		var p1Value = F.Rnd.Int;

		var predicates = ImmutableList.Create(new (IColumn, Compare, object)[]
		{
			( p0Column, p0Operator, p0Value ),
			( p1Column, p1Operator, p1Value )
		});

		var client = new MySqlDbClient();

		var expected = "SELECT" +
			$" `{c0Name}` AS '{c0Alias}', `{c1Name}` AS '{c1Alias}'" +
			$" FROM `{table}`" +
			$" WHERE `{p0Column.Name}` LIKE @P0" +
			$" AND `{p1Column.Name}` >= @P1;";

		// Act
		var (query, param) = client.GetQueryTest(table, list, predicates);

		// Assert
		Assert.Equal(expected, query);
		Assert.Collection(param,
			x =>
			{
				Assert.Equal("P0", x.Key);
				Assert.Equal(p0Value, x.Value);
			},
			x =>
			{
				Assert.Equal("P1", x.Key);
				Assert.Equal(p1Value, x.Value);
			}
		);
	}

	[Fact]
	public void With_Parts_Escapes_From_Table()
	{
		// Arrange
		var (client, table) = MySqlDbClient_Setup.Get();
		var parts = new QueryParts(table);

		// Act
		var (query, _) = client.GetQuery(parts);

		// Assert
		Assert.Contains(client.Escape(table.GetName()), query);
	}

	[Fact]
	public void With_Parts_SelectCount_True_Selects_Count()
	{
		// Arrange
		var (client, table) = MySqlDbClient_Setup.Get();
		var parts = new QueryParts(table) with { SelectCount = true };
		var expected = $"SELECT COUNT(*) FROM `{table.GetName()}`;";

		// Act
		var (query, _) = client.GetQuery(parts);

		// Assert
		Assert.Equal(expected, query);
	}

	[Fact]
	public void With_Parts_Select_Empty_Selects_All()
	{
		// Arrange
		var (client, table) = MySqlDbClient_Setup.Get();
		var parts = new QueryParts(table);
		var expected = $"SELECT * FROM `{table.GetName()}`;";

		// Act
		var (query, _) = client.GetQuery(parts);

		// Assert
		Assert.Equal(expected, query);
	}

	[Fact]
	public void With_Parts_Select_List_Escapes_With_Alias_And_Joins_Columns()
	{
		// Arrange
		var (client, table) = MySqlDbClient_Setup.Get();

		var c0Name = F.Rnd.Str;
		var c0Alias = F.Rnd.Str;
		var c0 = new Column(table, c0Name, c0Alias);
		var c1Name = F.Rnd.Str;
		var c1Alias = F.Rnd.Str;
		var c1 = new Column(table, c1Name, c1Alias);
		var parts = new QueryParts(table)
		{
			Select = new ColumnList(new[] { c0, c1 })
		};
		var expected = "SELECT" +
			$" `{table.GetName()}`.`{c0Name}` AS '{c0Alias}'," +
			$" `{table.GetName()}`.`{c1Name}` AS '{c1Alias}'" +
			$" FROM `{table.GetName()}`;";

		// Act
		var (query, _) = client.GetQuery(parts);

		// Assert
		Assert.Equal(expected, query);
	}

	private static void Test_Joins(Func<QueryParts, ImmutableList<(IColumn, IColumn)>, QueryParts> setJoin, string joinType)
	{
		// Arrange
		var (client, fromTable) = MySqlDbClient_Setup.Get();

		var fromName = F.Rnd.Str;
		var from = new Column(fromTable, fromName, F.Rnd.Str);

		var to0Table = F.Rnd.Str;
		var to0Name = F.Rnd.Str;
		var to0 = new Column(to0Table, to0Name, F.Rnd.Str);

		var to1Table = F.Rnd.Str;
		var to1Name = F.Rnd.Str;
		var to1 = new Column(to1Table, to1Name, F.Rnd.Str);

		var join = ImmutableList.Create(new (IColumn, IColumn)[] { (from, to0), (to0, to1) });

		var parts = setJoin(new(fromTable), join);

		var expected = "SELECT" +
			$" * FROM `{fromTable.GetName()}`" +
			$" {joinType} JOIN `{to0Table}` ON `{fromTable.GetName()}`.`{fromName}` = `{to0Table}`.`{to0Name}`" +
			$" {joinType} JOIN `{to1Table}` ON `{to0Table}`.`{to0Name}` = `{to1Table}`.`{to1Name}`;";

		// Act
		var (query, _) = client.GetQuery(parts);

		// Assert
		Assert.Equal(expected, query);
	}

	[Fact]
	public void With_Parts_Adds_Inner_Joins()
	{
		Test_Joins((p, j) => p with { InnerJoin = j }, "INNER");
	}

	[Fact]
	public void With_Parts_Adds_Left_Joins()
	{
		Test_Joins((p, j) => p with { LeftJoin = j }, "LEFT");
	}

	[Fact]
	public void With_Parts_Adds_Right_Joins()
	{
		Test_Joins((p, j) => p with { RightJoin = j }, "RIGHT");
	}

	[Fact]
	public void With_Parts_Adds_Where_Columns_With_Table_Names()
	{
		// Arrange
		var (client, fromTable) = MySqlDbClient_Setup.Get();

		var c0Table = F.Rnd.Str;
		var c0Name = F.Rnd.Str;
		var c0 = new Column(c0Table, c0Name, F.Rnd.Str);

		var c1Table = F.Rnd.Str;
		var c1Name = F.Rnd.Str;
		var c1 = new Column(c1Table, c1Name, F.Rnd.Str);

		var where = ImmutableList.Create(new (IColumn, Compare, object)[]
		{
			(c0, Compare.Like, F.Rnd.Str),
			(c1, Compare.MoreThanOrEqual, F.Rnd.Int)
		});

		var parts = new QueryParts(fromTable) { Where = where };

		var expected = $"SELECT * FROM `{fromTable.GetName()}` WHERE `{c0Table}`.`{c0Name}` LIKE @P0 AND `{c1Table}`.`{c1Name}` >= @P1;";

		// Act
		var (query, _) = client.GetQuery(parts);

		// Assert
		Assert.Equal(expected, query);
	}

	[Fact]
	public void With_Parts_Adds_Custom_Where_Clause_And_Parameters()
	{
		// Arrange
		var (client, fromTable) = MySqlDbClient_Setup.Get();

		var w0 = F.Rnd.Str;
		var w1 = F.Rnd.Str;
		var p0 = F.Rnd.Str;
		var p1 = F.Rnd.Str;
		var p2 = F.Rnd.Str;
		var parametersToAdd0 = new QueryParameters
		{
			{ nameof(p0), p0 },
			{ nameof(p1), p1 }
		};
		var parametersToAdd1 = new QueryParameters
		{
			{ nameof(p2), p2 }
		};

		var parts = new QueryParts(fromTable)
		{
			WhereCustom = ImmutableList.Create(new (string, IQueryParameters)[]
			{
				(w0, parametersToAdd0),
				(w1, parametersToAdd1)
			})
		};

		var expected = $"SELECT * FROM `{fromTable.GetName()}` WHERE ({w0}) AND ({w1});";

		// Act
		var (query, param) = client.GetQuery(parts);

		// Assert
		Assert.Equal(expected, query);
		Assert.Collection(param,
			x =>
			{
				Assert.Equal(nameof(p0), x.Key);
				Assert.Equal(p0, x.Value);
			},
			x =>
			{
				Assert.Equal(nameof(p1), x.Key);
				Assert.Equal(p1, x.Value);
			},
			x =>
			{
				Assert.Equal(nameof(p2), x.Key);
				Assert.Equal(p2, x.Value);
			}
		);
	}

	[Fact]
	public void With_Parts_Sets_Parameters()
	{
		// Arrange
		var (client, fromTable) = MySqlDbClient_Setup.Get();

		var c0Table = F.Rnd.Str;
		var c0Name = F.Rnd.Str;
		var c0Value = F.Rnd.Str;
		var c0 = new Column(c0Table, c0Name, F.Rnd.Str);

		var c1Table = F.Rnd.Str;
		var c1Name = F.Rnd.Str;
		var c1Value = F.Rnd.Int;
		var c1 = new Column(c1Table, c1Name, F.Rnd.Str);

		var where = ImmutableList.Create(new (IColumn, Compare, object)[]
		{
			(c0, Compare.Like, c0Value),
			(c1, Compare.MoreThanOrEqual, c1Value)
		});

		var parts = new QueryParts(fromTable) { Where = where };

		// Act
		var (_, param) = client.GetQuery(parts);

		// Assert
		Assert.Collection(param,
			x =>
			{
				Assert.Equal("P0", x.Key);
				Assert.Equal(c0Value, x.Value);
			},
			x =>
			{
				Assert.Equal("P1", x.Key);
				Assert.Equal(c1Value, x.Value);
			}
		);
	}

	[Fact]
	public void With_Parts_Adds_Order_By_Random()
	{
		// Arrange
		var (client, table) = MySqlDbClient_Setup.Get();
		var parts = new QueryParts(table)
		{
			SortRandom = true
		};

		var expected = $"SELECT * FROM `{table.GetName()}` ORDER BY RAND();";

		// Act
		var (query, _) = client.GetQuery(parts);

		// Assert
		Assert.Equal(expected, query);
	}

	[Fact]
	public void With_Parts_Adds_Order_By_With_Table_Name()
	{
		// Arrange
		var (client, table) = MySqlDbClient_Setup.Get();

		var sort0Table = F.Rnd.Str;
		var sort0Name = F.Rnd.Str;
		var sort0 = new Column(sort0Table, sort0Name, F.Rnd.Str);

		var sort1Table = F.Rnd.Str;
		var sort1Name = F.Rnd.Str;
		var sort1 = new Column(sort1Table, sort1Name, F.Rnd.Str);

		var parts = new QueryParts(table)
		{
			Sort = ImmutableList.Create(new (IColumn, SortOrder)[]
			{
				(sort0, SortOrder.Ascending),
				(sort1, SortOrder.Descending)
			})
		};

		var expected = "SELECT" +
			$" * FROM `{table.GetName()}` ORDER BY" +
			$" `{sort0Table}`.`{sort0Name}` ASC," +
			$" `{sort1Table}`.`{sort1Name}` DESC;";

		// Act
		var (query, _) = client.GetQuery(parts);

		// Assert
		Assert.Equal(expected, query);
	}

	[Fact]
	public void With_Parts_Adds_Limit()
	{
		// Arrange
		var (client, table) = MySqlDbClient_Setup.Get();
		var max = F.Rnd.Ulng;
		var parts = new QueryParts(table) { Maximum = max };
		var expected = $"SELECT * FROM `{table.GetName()}` LIMIT {max};";

		// Act
		var (query, _) = client.GetQuery(parts);

		// Assert
		Assert.Equal(expected, query);
	}

	[Fact]
	public void With_Parts_Adds_Limit_And_Offset()
	{
		// Arrange
		var (client, table) = MySqlDbClient_Setup.Get();
		var skip = F.Rnd.Ulng;
		var max = F.Rnd.Ulng;
		var parts = new QueryParts(table) { Skip = skip, Maximum = max };
		var expected = $"SELECT * FROM `{table.GetName()}` LIMIT {skip}, {max};";

		// Act
		var (query, _) = client.GetQuery(parts);

		// Assert
		Assert.Equal(expected, query);
	}
}
