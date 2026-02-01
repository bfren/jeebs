// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.Query;
using Jeebs.Functions;

namespace Jeebs.Data.Clients.Sqlite.SqliteDbClient_Tests;

public class GetQuery_Tests : SqliteDbClient_Setup
{
	[Fact]
	public void With_Predicates_Returns_Valid_Select_Query()
	{
		// Arrange
		var (client, v) = Setup();

		var schema = Rnd.Str;
		var name = Rnd.Str;
		var table = new TableName(schema, name);

		var c0Name = Rnd.Str;
		var c0Alias = Rnd.Str;
		var c0 = new Column(table, c0Name, Helpers.CreateInfoFromAlias(c0Alias));

		var c1Name = Rnd.Str;
		var c1Alias = Rnd.Str;
		var c1 = new Column(table, c1Name, Helpers.CreateInfoFromAlias(c1Alias));

		var list = new ColumnList([c0, c1]);

		var p0Column = new Column(table, Rnd.Str, Helpers.CreateInfoFromAlias());
		var p0Operator = Compare.Like;
		var p0Value = Rnd.Str;

		var p1Column = new Column(table, Rnd.Str, Helpers.CreateInfoFromAlias());
		var p1Operator = Compare.MoreThanOrEqual;
		var p1Value = Rnd.Int;

		var predicates = ListF.Create<(IColumn, Compare, dynamic)>(
		[
			( p0Column, p0Operator, p0Value ),
			( p1Column, p1Operator, p1Value )
		]);

		var expected = "SELECT" +
			$" \"{c0Name}\" AS \"{c0Alias}\"," +
			$" \"{c1Name}\" AS \"{c1Alias}\"" +
			$" FROM \"{schema}.{name}\"" +
			$" WHERE \"{p0Column.ColName}\" LIKE @P0" +
			$" AND \"{p1Column.ColName}\" >= @P1;";

		// Act
		var (query, param) = client.GetQueryTest(table, list, predicates).Unsafe().Unwrap();

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
		var (client, v) = Setup();

		var parts = new QueryParts(v.Table);

		// Act
		var (query, _) = client.GetQuery(parts).Unsafe().Unwrap();

		// Assert
		Assert.Contains($"\"{v.Schema}.{v.Name}\"", query);
	}

	[Fact]
	public void With_Parts_SelectCount_True_Selects_Count()
	{
		// Arrange
		var (client, v) = Setup();

		var parts = new QueryParts(v.Table) with { SelectCount = true };
		var expected = $"SELECT COUNT() FROM \"{v.Schema}.{v.Name}\";";

		// Act
		var (query, _) = client.GetQuery(parts).Unsafe().Unwrap();

		// Assert
		Assert.Equal(expected, query);
	}

	[Fact]
	public void With_Parts_Select_Empty_Selects_All()
	{
		// Arrange
		var (client, v) = Setup();

		var parts = new QueryParts(v.Table);
		var expected = $"SELECT * FROM \"{v.Schema}.{v.Name}\";";

		// Act
		var (query, _) = client.GetQuery(parts).Unsafe().Unwrap();

		// Assert
		Assert.Equal(expected, query);
	}

	[Fact]
	public void With_Parts_Select_List_Escapes_With_Alias_And_Joins_Columns()
	{
		// Arrange
		var (client, v) = Setup();

		var c0Name = Rnd.Str;
		var c0Alias = Rnd.Str;
		var c0 = new Column(v.Table, c0Name, Helpers.CreateInfoFromAlias(c0Alias));
		var c1Name = Rnd.Str;
		var c1Alias = Rnd.Str;
		var c1 = new Column(v.Table, c1Name, Helpers.CreateInfoFromAlias(c1Alias));
		var parts = new QueryParts(v.Table)
		{
			SelectColumns = new ColumnList([c0, c1])
		};
		var expected = "SELECT" +
			$" \"{v.Schema}.{v.Name}\".\"{c0Name}\" AS \"{c0Alias}\"," +
			$" \"{v.Schema}.{v.Name}\".\"{c1Name}\" AS \"{c1Alias}\"" +
			$" FROM \"{v.Schema}.{v.Name}\";";

		// Act
		var (query, _) = client.GetQuery(parts).Unsafe().Unwrap();

		// Assert
		Assert.Equal(expected, query);
	}

	private void Test_Joins(Func<QueryParts, ImmutableList<(IColumn, IColumn)>, QueryParts> setJoin, string joinType)
	{
		// Arrange
		var (client, v) = Setup();

		var fromName = Rnd.Str;
		var from = new Column(v.Table, fromName, Helpers.CreateInfoFromAlias());

		var to0Table = new TableName(Rnd.Str, Rnd.Str);
		var to0Name = Rnd.Str;
		IColumn to0 = new Column(to0Table, to0Name, Helpers.CreateInfoFromAlias());

		var to1Table = new TableName(Rnd.Str, Rnd.Str);
		var to1Name = Rnd.Str;
		IColumn to1 = new Column(to1Table, to1Name, Helpers.CreateInfoFromAlias());

		var join = ListF.Create([(from, to0), (to0, to1)]);

		var parts = setJoin(new(v.Table), join);

		var expected = "SELECT" +
			$" * FROM \"{v.Schema}.{v.Name}\"" +
			$" {joinType} JOIN \"{to0Table.Schema}.{to0Table.Name}\"" +
			$" ON \"{v.Schema}.{v.Name}\".\"{fromName}\" = \"{to0Table.Schema}.{to0Table.Name}\".\"{to0Name}\"" +
			$" {joinType} JOIN \"{to1Table.Schema}.{to1Table.Name}\"" +
			$" ON \"{to0Table.Schema}.{to0Table.Name}\".\"{to0Name}\" = \"{to1Table.Schema}.{to1Table.Name}\".\"{to1Name}\";";

		// Act
		var (query, _) = client.GetQuery(parts).Unsafe().Unwrap();

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
		var (client, v) = Setup();

		var c0Table = new TableName(Rnd.Str, Rnd.Str);
		var c0Name = Rnd.Str;
		var c0 = new Column(c0Table, c0Name, Helpers.CreateInfoFromAlias());

		var c1Table = new TableName(Rnd.Str, Rnd.Str);
		var c1Name = Rnd.Str;
		var c1 = new Column(c1Table, c1Name, Helpers.CreateInfoFromAlias());

		var where = ListF.Create<(IColumn, Compare, dynamic)>(
		[
			(c0, Compare.Like, Rnd.Str),
			(c1, Compare.MoreThanOrEqual, Rnd.Int)
		]);

		var parts = new QueryParts(v.Table) { Where = where };

		var expected = "SELECT *" +
			$" FROM \"{v.Schema}.{v.Name}\"" +
			$" WHERE \"{c0Table.Schema}.{c0Table.Name}\".\"{c0Name}\" LIKE @P0" +
			$" AND \"{c1Table.Schema}.{c1Table.Name}\".\"{c1Name}\" >= @P1;";

		// Act
		var (query, _) = client.GetQuery(parts).Unsafe().Unwrap();

		// Assert
		Assert.Equal(expected, query);
	}

	[Fact]
	public void With_Parts_Adds_Custom_Where_Clause_And_Parameters()
	{
		// Arrange
		var (client, v) = Setup();

		var w0 = Rnd.Str;
		var w1 = Rnd.Str;
		var p0 = Rnd.Str;
		var p1 = Rnd.Str;
		var p2 = Rnd.Str;
		IQueryParametersDictionary parametersToAdd0 = new QueryParametersDictionary
		{
			{ nameof(p0), p0 },
			{ nameof(p1), p1 }
		};
		IQueryParametersDictionary parametersToAdd1 = new QueryParametersDictionary
		{
			{ nameof(p2), p2 }
		};

		var parts = new QueryParts(v.Table)
		{
			WhereCustom = ListF.Create(
			[
				(w0, parametersToAdd0),
				(w1, parametersToAdd1)
			])
		};

		var expected = $"SELECT * FROM \"{v.Schema}.{v.Name}\" WHERE ({w0}) AND ({w1});";

		// Act
		var (query, param) = client.GetQuery(parts).Unsafe().Unwrap();

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
		var (client, v) = Setup();

		var c0Table = new TableName(Rnd.Str);
		var c0Name = Rnd.Str;
		var c0Value = Rnd.Str;
		var c0 = new Column(c0Table, c0Name, Helpers.CreateInfoFromAlias());

		var c1Table = new TableName(Rnd.Str);
		var c1Name = Rnd.Str;
		var c1Value = Rnd.Int;
		var c1 = new Column(c1Table, c1Name, Helpers.CreateInfoFromAlias());

		var where = ListF.Create<(IColumn, Compare, dynamic)>(
		[
			(c0, Compare.Like, c0Value),
			(c1, Compare.MoreThanOrEqual, c1Value)
		]);

		var parts = new QueryParts(v.Table) { Where = where };

		// Act
		var (_, param) = client.GetQuery(parts).Unsafe().Unwrap();

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
		var (client, v) = Setup();

		var parts = new QueryParts(v.Table)
		{
			SortRandom = true
		};

		var expected = $"SELECT * FROM \"{v.Schema}.{v.Name}\" ORDER BY random();";

		// Act
		var (query, _) = client.GetQuery(parts).Unsafe().Unwrap();

		// Assert
		Assert.Equal(expected, query);
	}

	[Fact]
	public void With_Parts_Adds_Order_By_With_Table_Name()
	{
		// Arrange
		var (client, v) = Setup();

		var sort0Table = new TableName(Rnd.Str, Rnd.Str);
		var sort0Name = Rnd.Str;
		IColumn sort0 = new Column(sort0Table, sort0Name, Helpers.CreateInfoFromAlias());

		var sort1Table = new TableName(Rnd.Str, Rnd.Str);
		var sort1Name = Rnd.Str;
		IColumn sort1 = new Column(sort1Table, sort1Name, Helpers.CreateInfoFromAlias());

		var parts = new QueryParts(v.Table)
		{
			Sort = ListF.Create(
			[
				(sort0, SortOrder.Ascending),
				(sort1, SortOrder.Descending)
			])
		};

		var expected = "SELECT" +
			$" * FROM \"{v.Schema}.{v.Name}\" ORDER BY" +
			$" \"{sort0Table.Schema}.{sort0Table.Name}\".\"{sort0Name}\" ASC," +
			$" \"{sort1Table.Schema}.{sort1Table.Name}\".\"{sort1Name}\" DESC;";

		// Act
		var (query, _) = client.GetQuery(parts).Unsafe().Unwrap();

		// Assert
		Assert.Equal(expected, query);
	}

	[Fact]
	public void With_Parts_Adds_Limit()
	{
		// Arrange
		var (client, v) = Setup();

		var max = Rnd.UInt64;
		var parts = new QueryParts(v.Table) { Maximum = max };
		var expected = $"SELECT * FROM \"{v.Schema}.{v.Name}\" LIMIT {max};";

		// Act
		var (query, _) = client.GetQuery(parts).Unsafe().Unwrap();

		// Assert
		Assert.Equal(expected, query);
	}

	[Fact]
	public void With_Parts_Adds_Limit_And_Offset()
	{
		// Arrange
		var (client, v) = Setup();

		var skip = Rnd.UInt64;
		var max = Rnd.UInt64;
		var parts = new QueryParts(v.Table) { Skip = skip, Maximum = max };
		var expected = $"SELECT * FROM \"{v.Schema}.{v.Name}\" LIMIT {skip}, {max};";

		// Act
		var (query, _) = client.GetQuery(parts).Unsafe().Unwrap();

		// Assert
		Assert.Equal(expected, query);
	}
}
