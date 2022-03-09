// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.Query.Exceptions;

namespace Jeebs.Data.Query.QueryBuilderWithFrom_Tests;

public class Where_Tests
{
	[Fact]
	public void Table_Not_Added_Throws_WhereTableNotAddedException()
	{
		// Arrange
		var table = Substitute.For<ITable>();
		var builder = new QueryBuilderWithFrom(table);

		// Act
		var action = void () => builder.Where<TestTable>(t => t.Foo, Compare.Equal, Rnd.Str);

		// Assert
		_ = Assert.Throws<WhereTableNotAddedException<TestTable>>(action);
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
		Assert.Collection(result.Parts.Where,
			x =>
			{
				Assert.Equal(nameof(TestTable), x.column.TblName.Name);
				Assert.Equal(TestTable.Prefix + nameof(TestTable0.Foo), x.column.ColName);
				Assert.Equal(nameof(TestTable.Foo), x.column.ColAlias);
				Assert.Equal(Compare.Like, x.cmp);
				Assert.Equal(value, x.value);
			}
		);
	}

	public sealed record class TestTable() : Table(nameof(TestTable))
	{
		public static readonly string Prefix = "Test";

		public string Foo { get; set; } = Prefix + nameof(Foo);
	}
}
