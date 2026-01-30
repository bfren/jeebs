// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Jeebs.Functions;

namespace Jeebs.Data.QueryBuilder.QueryPartsBuilder_Tests;

public abstract class AddWhereId_Tests<TBuilder, TId> : QueryPartsBuilder_Tests<TBuilder, TId>
	where TBuilder : QueryPartsBuilder<TId>
	where TId : ULongId<TId>, new()
{
	public abstract void Test00_Id_And_Ids_Null_Returns_Original_Parts();

	protected void Test00()
	{
		// Arrange
		var (builder, v) = Setup();

		// Act
		var result = builder.AddWhereId(v.Parts, default, ListF.Empty<TId>());

		// Assert
		var ok = result.AssertOk();
		Assert.Same(v.Parts, ok);
	}

	public abstract void Test01_Id_Set_Adds_Where_Id_Equal();

	protected void Test01()
	{
		// Arrange
		var id = IdGen.ULongId<TId>();
		var (builder, v) = Setup();

		// Act
		var result = builder.AddWhereId(v.Parts, id, ListF.Empty<TId>());

		// Assert
		var some = result.AssertOk();
		Assert.NotSame(v.Parts, some);
		var (column, compare, value) = Assert.Single(some.Where);
		Assert.Equal(builder.IdColumn, column);
		Assert.Equal(Compare.Equal, compare);
		Assert.Equal(id.Value, value);
	}

	public abstract void Test02_Id_And_Ids_Set_Adds_Where_Id_Equal();

	protected void Test02()
	{
		// Arrange
		var i0 = IdGen.ULongId<TId>();
		var i1 = IdGen.ULongId<TId>();
		var i2 = IdGen.ULongId<TId>();
		var ids = ListF.Create(i1, i2);
		var (builder, v) = Setup();

		// Act
		var result = builder.AddWhereId(v.Parts, i0, ids);

		// Assert
		var ok = result.AssertOk();
		Assert.NotSame(v.Parts, ok);
		var (column, compare, value) = Assert.Single(ok.Where);
		Assert.Equal(builder.IdColumn, column);
		Assert.Equal(Compare.Equal, compare);
		Assert.Equal(i0.Value, value);
	}

	public abstract void Test03_Id_Null_Ids_Set_Adds_Where_Id_In();

	protected void Test03()
	{
		// Arrange
		var i0 = IdGen.ULongId<TId>();
		var i1 = IdGen.ULongId<TId>();
		var ids = ListF.Create(i0, i1);
		var (builder, v) = Setup();

		// Act
		var result = builder.AddWhereId(v.Parts, default, ids);

		// Assert
		var ok = result.AssertOk();
		Assert.NotSame(v.Parts, ok);
		var (column, compare, value) = Assert.Single(ok.Where);
		Assert.Equal(builder.IdColumn, column);
		Assert.Equal(Compare.In, compare);
		Assert.Collection((IEnumerable<object>)value,
			y => Assert.Equal(i0.Value, y),
			y => Assert.Equal(i1.Value, y)
		);
	}
}
