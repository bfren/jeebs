// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;
using StrongId;

namespace Jeebs.Data.Query.QueryPartsBuilder_Tests;

public abstract class AddInnerJoin_Tests<TBuilder, TId> : QueryPartsBuilder_Tests<TBuilder, TId>
	where TBuilder : QueryPartsBuilder<TId>
	where TId : class, IStrongId, new()
{
	public abstract void Test00_Adds_Columns_To_InnerJoin();

	protected void Test00()
	{
		// Arrange
		var (builder, v) = Setup();

		var t0Name = new DbName(Rnd.Str);
		var t0Column = Rnd.Str;
		var t0 = new TestTable0(t0Name, t0Column);

		var t1Name = new DbName(Rnd.Str);
		var t1Column = Rnd.Str;
		var t1 = new TestTable1(t1Name, t1Column);

		// Act
		var result = builder.AddInnerJoin(v.Parts, t0, t => t.Foo, t1, t => t.Bar);

		// Assert
		var some = result.AssertSome();
		Assert.NotSame(v.Parts, some);
		var (from, to) = Assert.Single(some.InnerJoin);
		Assert.Equal(t0Name, from.TblName);
		Assert.Equal(t0Column, from.ColName);
		Assert.Equal(t1Name, to.TblName);
		Assert.Equal(t1Column, to.ColName);
	}
}
