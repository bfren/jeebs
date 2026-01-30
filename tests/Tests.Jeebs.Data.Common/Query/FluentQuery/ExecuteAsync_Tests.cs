// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using Jeebs.Data.Query;

namespace Jeebs.Data.Common.Query.FluentQuery_Tests;

public class ExecuteAsync_Tests : FluentQuery_Tests
{
	[Fact]
	public async Task Column_Does_Not_Exist__Returns_None_With_NoMatchingItemsMsg()
	{
		// Arrange
		var (query, _) = Setup();
		var alias = Rnd.Str;

		// Act
		var result = await query.ExecuteAsync<int>(alias);

		// Assert
		_ = result.AssertFailure("Column with alias '{Alias}' not found in table '{Table}'.", alias, nameof(TestTable));
	}

	[Fact]
	public async Task Column_Exists__Adds_SelectColumn_To_Parts()
	{
		// Arrange
		var (query, v) = Setup();
		var table = new TestTable();
		var withWhere = query.Update(parts => parts with
		{
			WhereCustom = parts.WhereCustom.WithItem((Rnd.Str, new QueryParametersDictionary()))
		});

		// Act
		await withWhere.ExecuteAsync<string>(nameof(TestEntity.Foo));
		await withWhere.ExecuteAsync(x => x.Foo);

		// Assert
		v.Db.Client.Received(2).GetQuery(Arg.Is<IQueryParts>(x =>
			x.SelectColumns.Count(y =>
				y.TblName == query.Table.GetName()
				&& y.ColName == table.Foo
				&& y.ColAlias == nameof(TestEntity.Foo)
			) == 1
		));
	}

	[Fact]
	public async Task Adds_Maximum_One__To_Parts()
	{
		// Arrange
		var (query, v) = Setup();
		var withWhere = query.Update(parts => parts with
		{
			WhereCustom = parts.WhereCustom.WithItem((Rnd.Str, new QueryParametersDictionary()))
		});

		// Act
		var r0 = await withWhere.ExecuteAsync<string>(nameof(TestEntity.Foo));
		var r1 = await withWhere.ExecuteAsync(x => x.Foo);

		// Assert
		v.Db.Client.Received(2).GetQuery(Arg.Is<IQueryParts>(x => x.Maximum == 1));
	}

	[Fact]
	public async Task Calls_Db_QueryAsync__With_Correct_Values()
	{
		// Arrange
		var sql = Rnd.Str;
		var param = new QueryParametersDictionary();
		var (query, v) = Setup(sql, param);
		var withWhere = query.Update(parts => parts with
		{
			WhereCustom = parts.WhereCustom.WithItem((Rnd.Str, new QueryParametersDictionary()))
		});

		// Act
		await withWhere.ExecuteAsync<string>(nameof(TestEntity.Foo));
		await withWhere.ExecuteAsync(x => x.Foo);

		// Assert
		await v.Db.Received(2).QueryAsync<string>(sql, param, CommandType.Text, v.Transaction);
	}
}
