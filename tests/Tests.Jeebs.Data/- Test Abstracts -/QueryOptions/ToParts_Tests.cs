// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using NSubstitute;
using NSubstitute.Extensions;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public abstract class ToParts_Tests<TOptions, TBuilder, TId>
		where TOptions : QueryOptions<TId>
		where TBuilder : class, IQueryPartsBuilder<TId>
		where TId : StrongId, new()
	{
		protected abstract (TOptions options, TBuilder builder) Setup();

#pragma warning disable NS1004 // Argument matcher used with a non-virtual member of a class.
		protected QueryParts Qp =>
			Arg.Any<QueryParts>();
#pragma warning restore NS1004 // Argument matcher used with a non-virtual member of a class.

		protected virtual TBuilder GetConfiguredBuilder(ITable table)
		{
			var parts = new QueryParts(table)
			{
				Maximum = F.Rnd.Lng,
				Skip = F.Rnd.Lng
			};

			var builder = Substitute.For<TBuilder>();
			builder.Create<TestModel>(Arg.Any<long?>(), Arg.Any<long>()).Returns(parts);
			builder.ReturnsForAll(parts.Return());

			return builder;
		}

		public abstract void Test00_Calls_Builder_Create_With_Maximum_And_Skip();

		protected void Test00()
		{
			// Arrange
			var (options, builder) = Setup();
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
			var id = new TId { Value = F.Rnd.Lng };
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
			builder.Received().AddWhereId(Qp, null, ids);
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
			builder.Received().AddSort(Qp, false, sort);
		}
	}

	public sealed record TestModel;
}
