// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying.Exceptions;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryBuilderWithFrom_Tests;

public class Join_Tests
{
	[Fact]
	public void From_Table_Not_Added_Throws_JoinFromTableNotAddedException()
	{
		// Arrange
		var table = Substitute.For<ITable>();
		var builder = new QueryBuilderWithFrom(table);

		// Act
		void action() => builder.Join<TestTable0, TestTable0>(QueryJoin.Inner, t => t.Foo, t => t.Foo);

		// Assert
		Assert.Throws<JoinFromTableNotAddedException<TestTable0>>(action);
	}

	[Fact]
	public void Adds_To_Table_If_Not_Already_Added()
	{
		// Arrange
		var table = new TestTable0();
		var builder = new QueryBuilderWithFrom(table);

		// Act
		builder.Join<TestTable0, TestTable1>(QueryJoin.Inner, t => t.Foo, t => t.Bar);

		// Assert
		Assert.Collection(builder.Tables,
			x => Assert.IsType<TestTable0>(x),
			x => Assert.IsType<TestTable1>(x)
		);
	}

	private static void Test_Add_Join(QueryJoin join, Func<IQueryParts, IImmutableList<(IColumn from, IColumn to)>> getJoin)
	{
		// Arrange
		var table = new TestTable0();
		var builder = new QueryBuilderWithFrom(table);

		// Act
		var result = (QueryBuilderWithFrom)builder.Join<TestTable0, TestTable1>(join, t => t.Foo, t => t.Bar);

		// Assert
		Assert.Collection(getJoin(result.Parts),
			x =>
			{
				Assert.Equal("TestTable0", x.from.Table);
				Assert.Equal("TestFoo", x.from.Name);
				Assert.Equal("Foo", x.from.Alias);
				Assert.Equal("TestTable1", x.to.Table);
				Assert.Equal("TestBar", x.to.Name);
				Assert.Equal("Bar", x.to.Alias);
			}
		);
	}

	[Fact]
	public void Adds_Inner_Join()
	{
		Test_Add_Join(QueryJoin.Inner, x => x.InnerJoin);
	}

	[Fact]
	public void Adds_Left_Join()
	{
		Test_Add_Join(QueryJoin.Left, x => x.LeftJoin);
	}

	[Fact]
	public void Adds_Right_Join()
	{
		Test_Add_Join(QueryJoin.Right, x => x.RightJoin);
	}

	public sealed record class TestTable0() : Table(nameof(TestTable0))
	{
		public const string Prefix = "Test";

		public string Foo { get; set; } = Prefix + nameof(Foo);
	}

	public sealed record class TestTable1() : Table(nameof(TestTable1))
	{
		public const string Prefix = "Test";

		public string Bar { get; set; } = Prefix + nameof(Bar);
	}
}
