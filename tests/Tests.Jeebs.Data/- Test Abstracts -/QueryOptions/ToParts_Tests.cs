// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Collections;
using Jeebs.Data.Enums;
using Jeebs.Functions;

namespace Jeebs.Data.Query.QueryOptions_Tests;

public abstract class ToParts_Tests<TOptions, TBuilder, TId> : QueryOptions_Tests<TOptions, TBuilder, TId>
	where TOptions : QueryOptions<TId>
	where TBuilder : class, IQueryPartsBuilder<TId>
	where TId : ULongId<TId>, new()
{
	public abstract void Test00_Calls_Builder_Create_With_Maximum_And_Skip();

	protected void Test00()
	{
		// Arrange
		var (options, builder) = Setup();
		var max = Rnd.UInt64;
		var skip = Rnd.UInt64;
		var opt = options with
		{
			Maximum = max,
			Skip = skip
		};

		// Act
		opt.ToParts<TestModel>();

		// Assert
		builder.Received().Create<TestModel>(max, skip);
	}

	public abstract void Test01_Id_Null_Ids_Empty_Does_Not_Call_Builder_AddWhereId();

	protected void Test01()
	{
		// Arrange
		var (options, builder) = Setup();

		// Act
		options.ToParts<TestModel>();

		// Assert
		builder.DidNotReceiveWithAnyArgs().AddWhereId(Qp, default, Arg.Any<IImmutableList<TId>>());
	}

	public abstract void Test02_Id_Not_Null_Calls_Builder_AddWhereId();

	protected void Test02()
	{
		// Arrange
		var (options, builder) = Setup();
		var id = IdGen.ULongId<TId>();
		var opt = options with
		{
			Id = id
		};

		// Act
		opt.ToParts<TestModel>();

		// Assert
		builder.Received().AddWhereId(Qp, id, Arg.Any<IImmutableList<TId>>());
	}

	public abstract void Test03_Ids_Not_Empty_Calls_Builder_AddWhereId();

	protected void Test03()
	{
		// Arrange
		var (options, builder) = Setup();
		var i0 = IdGen.ULongId<TId>();
		var i1 = IdGen.ULongId<TId>();
		var ids = ListF.Create(i0, i1);
		var opt = options with
		{
			Ids = ids
		};

		// Act
		opt.ToParts<TestModel>();

		// Assert
		builder.Received().AddWhereId(Qp, default, ids);
	}

	public abstract void Test04_SortRandom_False_Sort_Empty_Does_Not_Call_Builder_AddSort();

	protected void Test04()
	{
		// Arrange
		var (options, builder) = Setup();

		// Act
		options.ToParts<TestModel>();

		// Assert
		builder.DidNotReceive().AddSort(Qp, Arg.Any<bool>(), Arg.Any<IImmutableList<(IColumn, SortOrder)>>());
	}

	public abstract void Test05_SortRandom_True_Calls_Builder_AddSort();

	protected void Test05()
	{
		// Arrange
		var (options, builder) = Setup();
		var opt = options with
		{
			SortRandom = true
		};

		// Act
		opt.ToParts<TestModel>();

		// Assert
		builder.Received().AddSort(Qp, true, Arg.Any<IImmutableList<(IColumn, SortOrder)>>());
	}

	public abstract void Test06_Sort_Not_Empty_Calls_Builder_AddSort();

	protected void Test06()
	{
		// Arrange
		var (options, builder) = Setup();
		var sort = ListF.Create(
			(Substitute.For<IColumn>(), SortOrder.Ascending),
			(Substitute.For<IColumn>(), SortOrder.Descending)
		);
		var opt = options with
		{
			Sort = sort
		};

		// Act
		opt.ToParts<TestModel>();

		// Assert
		builder.Received().AddSort(Qp, false, sort);
	}
}

public sealed record class TestModel;
