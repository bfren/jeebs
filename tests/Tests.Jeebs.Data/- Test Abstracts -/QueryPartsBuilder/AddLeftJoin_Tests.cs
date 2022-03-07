﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Mapping;
using Xunit;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests;

public abstract class AddLeftJoin_Tests<TBuilder, TId> : QueryPartsBuilder_Tests<TBuilder, TId>
	where TBuilder : QueryPartsBuilder<TId>
	where TId : IStrongId
{
	public abstract void Test00_Adds_Columns_To_LeftJoin();

	protected void Test00()
	{
		// Arrange
		var (builder, v) = Setup();

		var t0Name = new TableName(F.Rnd.Str);
		var t0Column = F.Rnd.Str;
		var t0 = new TestTable0(t0Name, t0Column);

		var t1Name = new TableName(F.Rnd.Str);
		var t1Column = F.Rnd.Str;
		var t1 = new TestTable1(t1Name, t1Column);

		// Act
		var result = builder.AddLeftJoin(v.Parts, t0, t => t.Foo, t1, t => t.Bar);

		// Assert
		var some = result.AssertSome();
		Assert.NotSame(v.Parts, some);
		Assert.Collection(some.LeftJoin,
			x =>
			{
				Assert.Equal(t0Name, x.from.TblName);
				Assert.Equal(t0Column, x.from.ColName);

				Assert.Equal(t1Name, x.to.TblName);
				Assert.Equal(t1Column, x.to.ColName);
			}
		);
	}
}
