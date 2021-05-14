// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using NSubstitute;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public abstract class ToParts_Tests<TOptions, TBuilder, TId>
		where TOptions : QueryOptions<TId>
		where TBuilder : class, IQueryPartsBuilder<TId>
		where TId : StrongId, new()
	{
		public abstract void Test00_Calls_Builder_Create_With_Maximum_And_Skip();

		protected void Test00(TOptions options, TBuilder builder)
		{
			// Arrange
			var max = F.Rnd.Lng;
			var skip = F.Rnd.Lng;
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

		protected void Test01(TOptions options, TBuilder builder)
		{
			// Arrange

			// Act
			options.ToParts<TestModel>();

			// Assert
			builder.DidNotReceive().AddWhereId(Arg.Any<QueryParts>(), Arg.Any<TId?>(), Arg.Any<IImmutableList<TId>>());
		}

		public abstract void Test02_Id_Not_Null_Calls_Builder_AddWhereId();

		protected void Test02(TOptions options, TBuilder builder)
		{
			// Arrange
			var id = new TId { Value = F.Rnd.Lng };
			var opt = options with
			{
				Id = id
			};

			// Act
			opt.ToParts<TestModel>();

			// Assert
			builder.Received().AddWhereId(Arg.Any<QueryParts>(), id, Arg.Any<IImmutableList<TId>>());
		}

		public abstract void Test03_Ids_Not_Empty_Calls_Builder_AddWhereId();

		protected void Test03(TOptions options, TBuilder builder)
		{
			// Arrange
			var i0 = new TId { Value = F.Rnd.Lng };
			var i1 = new TId { Value = F.Rnd.Lng };
			var ids = ImmutableList.Create(i0, i1);
			var opt = options with
			{
				Ids = ids
			};

			// Act
			opt.ToParts<TestModel>();

			// Assert
			builder.Received().AddWhereId(Arg.Any<QueryParts>(), null, ids);
		}

		public abstract void Test04_SortRandom_False_Sort_Empty_Does_Not_Call_Builder_AddSort();

		protected void Test04(TOptions options, TBuilder builder)
		{
			// Arrange

			// Act
			options.ToParts<TestModel>();

			// Assert
			builder.DidNotReceive().AddSort(Arg.Any<QueryParts>(), Arg.Any<bool>(), Arg.Any<IImmutableList<(IColumn, SortOrder)>>());
		}

		public abstract void Test05_SortRandom_True_Calls_Builder_AddSort();

		protected void Test05(TOptions options, TBuilder builder)
		{
			// Arrange
			var opt = options with
			{
				SortRandom = true
			};

			// Act
			opt.ToParts<TestModel>();

			// Assert
			builder.Received().AddSort(Arg.Any<QueryParts>(), true, Arg.Any<IImmutableList<(IColumn, SortOrder)>>());
		}

		public abstract void Test06_Sort_Not_Empty_Calls_Builder_AddSort();

		protected void Test06(TOptions options, TBuilder builder)
		{
			// Arrange
			var sort = ImmutableList.Create(
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
			builder.Received().AddSort(Arg.Any<QueryParts>(), false, sort);
		}
	}

	public sealed record TestModel;
}
