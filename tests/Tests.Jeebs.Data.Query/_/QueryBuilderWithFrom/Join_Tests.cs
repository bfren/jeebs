﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Data.Map;
using Jeebs.Data.Query.Exceptions;

namespace Jeebs.Data.Query.QueryBuilderWithFrom_Tests;

public class Join_Tests
{
	[Fact]
	public void From_Table_Not_Added_Throws_JoinFromTableNotAddedException()
	{
		// Arrange
		var table = Substitute.For<ITable>();
		var builder = new QueryBuilderWithFrom(table);

		// Act
		var action = void () => builder.Join<TestTable0, TestTable0>(QueryJoin.Inner, t => t.Foo, t => t.Foo);

		// Assert
		_ = Assert.Throws<JoinFromTableNotAddedException<TestTable0>>(action);
	}

	[Fact]
	public void Adds_To_Table_If_Not_Already_Added()
	{
		// Arrange
		var table = new TestTable0();
		var builder = new QueryBuilderWithFrom(table);

		// Act
		_ = builder.Join<TestTable0, TestTable1>(QueryJoin.Inner, t => t.Foo, t => t.Bar);

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
				Assert.Equal(nameof(TestTable0), x.from.TblName.Name);
				Assert.Equal(TestTable0.Prefix + nameof(TestTable0.Foo), x.from.ColName);
				Assert.Equal(nameof(TestTable0.Foo), x.from.ColAlias);
				Assert.Equal(nameof(TestTable1), x.to.TblName.Name);
				Assert.Equal(TestTable1.Prefix + nameof(TestTable1.Bar), x.to.ColName);
				Assert.Equal(nameof(TestTable1.Bar), x.to.ColAlias);
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
		public static readonly string Prefix = "Test";

		public string Foo { get; set; } = Prefix + nameof(Foo);
	}

	public sealed record class TestTable1() : Table(nameof(TestTable1))
	{
		public static readonly string Prefix = "Test";

		public string Bar { get; set; } = Prefix + nameof(Bar);
	}
}
