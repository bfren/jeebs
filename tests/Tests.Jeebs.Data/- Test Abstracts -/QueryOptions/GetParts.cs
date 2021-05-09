// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public abstract class GetParts<TOptions, TId> : QueryOptions_Tests<TOptions, TId>
		where TOptions : QueryOptions<TId>
		where TId : StrongId, new()
	{
		public abstract void Test00_Adds_Id();

		protected void Test00()
		{
			// Arrange
			var id = new TId { Value = F.Rnd.Lng };
			var (options, v) = Setup(opt => opt with { Id = id });
			var cols = Substitute.For<IColumnList>();

			// Act
			var result = options.GetPartsTest(v.Table, cols, v.IdColumn);

			// Assert
			var some = result.AssertSome();
			Assert.Collection(some.Where,
				x =>
				{
					Assert.Same(v.IdColumn, x.column);
					Assert.Equal(Compare.Equal, x.cmp);
					Assert.Equal(id.Value, x.value);
				}
			);
		}

		public abstract void Test01_Adds_Ids();

		protected void Test01()
		{
			// Arrange
			var i0 = new TId { Value = F.Rnd.Lng };
			var i1 = new TId { Value = F.Rnd.Lng };
			var (options, v) = Setup(opt => opt with { Ids = new[] { i0, i1 } });
			var cols = Substitute.For<IColumnList>();

			// Act
			var result = options.GetPartsTest(v.Table, cols, v.IdColumn);

			// Assert
			var some = result.AssertSome();
			Assert.Collection(some.Where,
				x =>
				{
					Assert.Same(v.IdColumn, x.column);
					Assert.Equal(Compare.In, x.cmp);

					var value = Assert.IsAssignableFrom<IEnumerable<long>>(x.value);
					Assert.Collection(value,
						y => Assert.Equal(i0.Value, y),
						y => Assert.Equal(i1.Value, y)
					);
				}
			);
		}

		public abstract void Test02_Adds_SortRandom();

		protected void Test02()
		{
			// Arrange
			var (options, v) = Setup(opt => opt with { SortRandom = true });
			var cols = Substitute.For<IColumnList>();

			// Act
			var result = options.GetPartsTest(v.Table, cols, v.IdColumn);

			// Assert
			var some = result.AssertSome();
			Assert.True(some.SortRandom);
		}

		public abstract void Test03_Adds_Sort();

		protected void Test03()
		{
			// Arrange
			var c0 = Substitute.For<IColumn>();
			var o0 = SortOrder.Ascending;
			var c1 = Substitute.For<IColumn>();
			var o1 = SortOrder.Descending;
			var sort = new[] { (c0, o0), (c1, o1) };
			var (options, v) = Setup(opt => opt with { Sort = sort });
			var cols = Substitute.For<IColumnList>();

			// Act
			var result = options.GetPartsTest(v.Table, cols, v.IdColumn);

			// Assert
			var some = result.AssertSome();
			Assert.Collection(some.Sort,
				x =>
				{
					Assert.Same(c0, x.column);
					Assert.Equal(o0, x.order);
				},
				x =>
				{
					Assert.Same(c1, x.column);
					Assert.Equal(o1, x.order);
				}
			);
		}
	}
}
