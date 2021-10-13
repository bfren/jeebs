// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying.Exceptions;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryBuilderWithFrom_Tests;

public class Where_Tests
{
	[Fact]
	public void Table_Not_Added_Throws_WhereTableNotAddedException()
	{
		// Arrange
		var table = Substitute.For<ITable>();
		var builder = new QueryBuilderWithFrom(table);

		// Act
		void action() => builder.Where<TestTable>(t => t.Foo, Compare.Equal, F.Rnd.Str);

		// Assert
		Assert.Throws<WhereTableNotAddedException<TestTable>>(action);
	}

	[Fact]
	public void Adds_Predicate_To_Where_List()
	{
		// Arrange
		var table = new TestTable();
		var builder = new QueryBuilderWithFrom(table);
		var value = F.Rnd.Str;

		// Act
		var result = (QueryBuilderWithFrom)builder.Where<TestTable>(t => t.Foo, Compare.Like, value);

		// Assert
		Assert.Collection(result.Parts.Where,
			x =>
			{
				Assert.Equal("TestTable", x.column.Table);
				Assert.Equal("TestFoo", x.column.Name);
				Assert.Equal("Foo", x.column.Alias);
				Assert.Equal(Compare.Like, x.cmp);
				Assert.Equal(value, x.value);
			}
		);
	}

	public sealed record class TestTable() : Table(nameof(TestTable))
	{
		public const string Prefix = "Test";

		public string Foo { get; set; } = Prefix + nameof(Foo);
	}
}
