// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using Xunit;

namespace Jeebs.Data.Clients.SqlServer.SqlServerDbClient_Tests;

public class GetQuery_Tests
{
	[Fact]
	public void With_Predicates_Returns_Valid_Select_Query()
	{
		// Arrange
		var schema = F.Rnd.Str;
		var name = F.Rnd.Str;
		var table = new TableName(schema, name);

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

		var client = new SqlServerDbClient();

		var expected = "SELECT" +
			$" [{c0Name}] AS [{c0Alias}]," +
			$" [{c1Name}] AS [{c1Alias}]" +
			$" FROM [{schema}].[{name}]" +
			$" WHERE [{p0Column.Name}] LIKE @P0" +
			$" AND [{p1Column.Name}] >= @P1";

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
		var (client, v) = SqlServerDbClient_Setup.Get();
		var parts = new QueryParts(v.Table);

		// Act
		var (query, _) = client.GetQuery(parts);

		// Assert
		Assert.Contains($"[{v.Schema}].[{v.Name}]", query);
	}

	[Fact]
	public void With_Parts_SelectCount_True_Selects_Count()
	{
		// Arrange
		var (client, v) = SqlServerDbClient_Setup.Get();
		var parts = new QueryParts(v.Table) with { SelectCount = true };
		var expected = $"SELECT COUNT(*) FROM [{v.Schema}].[{v.Name}]";

		// Act
		var (query, _) = client.GetQuery(parts);

		// Assert
		Assert.Equal(expected, query);
	}

	[Fact]
	public void With_Parts_Select_Empty_Selects_All()
	{
		// Arrange
		var (client, v) = SqlServerDbClient_Setup.Get();
		var parts = new QueryParts(v.Table);
		var expected = $"SELECT * FROM [{v.Schema}].[{v.Name}]";

		// Act
		var (query, _) = client.GetQuery(parts);

		// Assert
		Assert.Equal(expected, query);
	}

	[Fact]
	public void With_Parts_Select_List_Escapes_With_Alias_And_Joins_Columns()
	{
		// Arrange
		var (client, v) = SqlServerDbClient_Setup.Get();

		var c0Name = F.Rnd.Str;
		var c0Alias = F.Rnd.Str;
		var c0 = new Column(v.Table, c0Name, c0Alias);
		var c1Name = F.Rnd.Str;
		var c1Alias = F.Rnd.Str;
		var c1 = new Column(v.Table, c1Name, c1Alias);
		var parts = new QueryParts(v.Table)
		{
			Select = new ColumnList(new[] { c0, c1 })
		};
		var expected = "SELECT" +
			$" [{v.Schema}].[{v.Name}].[{c0Name}] AS [{c0Alias}]," +
			$" [{v.Schema}].[{v.Name}].[{c1Name}] AS [{c1Alias}]" +
			$" FROM [{v.Schema}].[{v.Name}]";

		// Act
		var (query, _) = client.GetQuery(parts);

		// Assert
		Assert.Equal(expected, query);
	}

	private static void Test_Joins(Func<QueryParts, ImmutableList<(IColumn, IColumn)>, QueryParts> setJoin, string joinType)
	{
		// Arrange
		var (client, v) = SqlServerDbClient_Setup.Get();

		var fromName = F.Rnd.Str;
		var from = new Column(v.Table, fromName, F.Rnd.Str);

		var to0Table = new TableName(F.Rnd.Str, F.Rnd.Str);
		var to0Name = F.Rnd.Str;
		var to0 = new Column(to0Table, to0Name, F.Rnd.Str);

		var to1Table = new TableName(F.Rnd.Str, F.Rnd.Str);
		var to1Name = F.Rnd.Str;
		var to1 = new Column(to1Table, to1Name, F.Rnd.Str);

		var join = ImmutableList.Create(new (IColumn, IColumn)[] { (from, to0), (to0, to1) });

		var parts = setJoin(new(v.Table), join);

		var expected = "SELECT" +
			$" * FROM [{v.Schema}].[{v.Name}]" +
			$" {joinType} JOIN [{to0Table.Schema}].[{to0Table.Name}]" +
			$" ON [{v.Schema}].[{v.Name}].[{fromName}] = [{to0Table.Schema}].[{to0Table.Name}].[{to0Name}]" +
			$" {joinType} JOIN [{to1Table.Schema}].[{to1Table.Name}]" +
			$" ON [{to0Table.Schema}].[{to0Table.Name}].[{to0Name}] = [{to1Table.Schema}].[{to1Table.Name}].[{to1Name}]";

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
		var (client, v) = SqlServerDbClient_Setup.Get();

		var c0Table = new TableName(F.Rnd.Str, F.Rnd.Str);
		var c0Name = F.Rnd.Str;
		var c0 = new Column(c0Table, c0Name, F.Rnd.Str);

		var c1Table = new TableName(F.Rnd.Str, F.Rnd.Str);
		var c1Name = F.Rnd.Str;
		var c1 = new Column(c1Table, c1Name, F.Rnd.Str);

		var where = ImmutableList.Create(new (IColumn, Compare, object)[]
		{
			(c0, Compare.Like, F.Rnd.Str),
			(c1, Compare.MoreThanOrEqual, F.Rnd.Int)
		});

		var parts = new QueryParts(v.Table) { Where = where };

		var expected = "SELECT *" +
			$" FROM [{v.Schema}].[{v.Name}]" +
			$" WHERE [{c0Table.Schema}].[{c0Table.Name}].[{c0Name}] LIKE @P0" +
			$" AND [{c1Table.Schema}].[{c1Table.Name}].[{c1Name}] >= @P1";

		// Act
		var (query, _) = client.GetQuery(parts);

		// Assert
		Assert.Equal(expected, query);
	}

	[Fact]
	public void With_Parts_Adds_Custom_Where_Clause_And_Parameters()
	{
		// Arrange
		var (client, v) = SqlServerDbClient_Setup.Get();

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

		var parts = new QueryParts(v.Table)
		{
			WhereCustom = ImmutableList.Create(new (string, IQueryParameters)[]
			{
				(w0, parametersToAdd0),
				(w1, parametersToAdd1)
			})
		};

		var expected = $"SELECT * FROM [{v.Schema}].[{v.Name}] WHERE ({w0}) AND ({w1})";

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
		var (client, v) = SqlServerDbClient_Setup.Get();

		var c0Table = new TableName(F.Rnd.Str);
		var c0Name = F.Rnd.Str;
		var c0Value = F.Rnd.Str;
		var c0 = new Column(c0Table, c0Name, F.Rnd.Str);

		var c1Table = new TableName(F.Rnd.Str);
		var c1Name = F.Rnd.Str;
		var c1Value = F.Rnd.Int;
		var c1 = new Column(c1Table, c1Name, F.Rnd.Str);

		var where = ImmutableList.Create(new (IColumn, Compare, object)[]
		{
			(c0, Compare.Like, c0Value),
			(c1, Compare.MoreThanOrEqual, c1Value)
		});

		var parts = new QueryParts(v.Table) { Where = where };

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
		var (client, v) = SqlServerDbClient_Setup.Get();
		var parts = new QueryParts(v.Table)
		{
			SortRandom = true
		};

		var expected = $"SELECT * FROM [{v.Schema}].[{v.Name}] ORDER BY NEWID()";

		// Act
		var (query, _) = client.GetQuery(parts);

		// Assert
		Assert.Equal(expected, query);
	}

	[Fact]
	public void With_Parts_Adds_Order_By_With_Table_Name()
	{
		// Arrange
		var (client, v) = SqlServerDbClient_Setup.Get();

		var sort0Table = new TableName(F.Rnd.Str, F.Rnd.Str);
		var sort0Name = F.Rnd.Str;
		var sort0 = new Column(sort0Table, sort0Name, F.Rnd.Str);

		var sort1Table = new TableName(F.Rnd.Str, F.Rnd.Str);
		var sort1Name = F.Rnd.Str;
		var sort1 = new Column(sort1Table, sort1Name, F.Rnd.Str);

		var parts = new QueryParts(v.Table)
		{
			Sort = ImmutableList.Create(new (IColumn, SortOrder)[]
			{
				(sort0, SortOrder.Ascending),
				(sort1, SortOrder.Descending)
			})
		};

		var expected = "SELECT" +
			$" * FROM [{v.Schema}].[{v.Name}] ORDER BY" +
			$" [{sort0Table.Schema}].[{sort0Table.Name}].[{sort0Name}] ASC," +
			$" [{sort1Table.Schema}].[{sort1Table.Name}].[{sort1Name}] DESC";

		// Act
		var (query, _) = client.GetQuery(parts);

		// Assert
		Assert.Equal(expected, query);
	}

	[Fact]
	public void With_Parts_Adds_Limit()
	{
		// Arrange
		var (client, v) = SqlServerDbClient_Setup.Get();
		var max = F.Rnd.Ulng;
		var parts = new QueryParts(v.Table) { Maximum = max };
		var expected = $"SELECT * FROM [{v.Schema}].[{v.Name}] OFFSET 0 ROWS FETCH NEXT {max} ROWS ONLY";

		// Act
		var (query, _) = client.GetQuery(parts);

		// Assert
		Assert.Equal(expected, query);
	}

	[Fact]
	public void With_Parts_Adds_Limit_And_Offset()
	{
		// Arrange
		var (client, v) = SqlServerDbClient_Setup.Get();
		var skip = F.Rnd.Ulng;
		var max = F.Rnd.Ulng;
		var parts = new QueryParts(v.Table) { Skip = skip, Maximum = max };
		var expected = $"SELECT * FROM [{v.Schema}].[{v.Name}] OFFSET {skip} ROWS FETCH NEXT {max} ROWS ONLY";

		// Act
		var (query, _) = client.GetQuery(parts);

		// Assert
		Assert.Equal(expected, query);
	}
}
