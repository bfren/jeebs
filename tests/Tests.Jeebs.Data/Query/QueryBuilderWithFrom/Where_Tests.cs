// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.QueryBuilder.Exceptions;

namespace Jeebs.Data.QueryBuilder.QueryBuilderWithFrom_Tests;

public class Where_Tests
{
	[Fact]
	public void Table_Not_Added_Throws_WhereTableNotAddedException()
	{
		// Arrange
		var table = Substitute.For<ITable>();
		var builder = new QueryBuilderWithFrom(table);

		// Act
		var result = Record.Exception(() => builder.Where<TestTable>(t => t.Foo, Compare.Equal, Rnd.Str));

		// Assert
		Assert.IsType<WhereTableNotAddedException<TestTable>>(result);
	}

	[Fact]
	public void Adds_Predicate_To_Where_List()
	{
		// Arrange
		var table = new TestTable();
		var builder = new QueryBuilderWithFrom(table);
		var value = Rnd.Str;

		// Act
		var result = (QueryBuilderWithFrom)builder.Where<TestTable>(t => t.Foo, Compare.Like, value);

		// Assert
		var single = Assert.Single(result.Parts.Where);
		Assert.Equal(nameof(TestTable), single.column.TblName.Name);
		Assert.Equal(TestTable.Prefix + nameof(TestTable0.Foo), single.column.ColName);
		Assert.Equal(nameof(TestTable.Foo), single.column.ColAlias);
		Assert.Equal(Compare.Like, single.compare);
		Assert.Equal(value, single.value);
	}

	public sealed record class TestTable() : Table(nameof(TestTable))
	{
		public static readonly string Prefix = "Test";

		public string Foo { get; set; } = Prefix + nameof(Foo);
	}
}
