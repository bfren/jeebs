// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public class GetParts_Tests
	{
		[Fact]
		public void Adds_Id()
		{
			// Arrange
			var id = F.Rnd.Lng;
			var (table, _, idColumn, _, options) = QueryOptions_Setup.Get(opt => opt with { Id = new(id) });
			var cols = Substitute.For<IColumnList>();

			// Act
			var result = options.GetPartsTest(table, cols, idColumn);

			// Assert
			var some = result.AssertSome();
			Assert.Collection(some.Where,
				x =>
				{
					Assert.Same(idColumn, x.column);
					Assert.Equal(Compare.Equal, x.cmp);
					Assert.Equal(id, x.value);
				}
			);
		}

		[Fact]
		public void Adds_Ids()
		{
			// Arrange
			var i0 = F.Rnd.Lng;
			var i1 = F.Rnd.Lng;
			var ids = new QueryOptions_Setup.TestId[] { new(i0), new(i1) };
			var (table, _, idColumn, _, options) = QueryOptions_Setup.Get(opt => opt with { Ids = ids });
			var cols = Substitute.For<IColumnList>();

			// Act
			var result = options.GetPartsTest(table, cols, idColumn);

			// Assert
			var some = result.AssertSome();
			Assert.Collection(some.Where,
				x =>
				{
					Assert.Same(idColumn, x.column);
					Assert.Equal(Compare.In, x.cmp);

					var value = Assert.IsAssignableFrom<IEnumerable<long>>(x.value);
					Assert.Collection(value,
						y => Assert.Equal(i0, y),
						y => Assert.Equal(i1, y)
					);
				}
			);
		}

		[Fact]
		public void Adds_SortRandom()
		{
			// Arrange
			var (table, _, idColumn, _, options) = QueryOptions_Setup.Get(opt => opt with { SortRandom = true });
			var cols = Substitute.For<IColumnList>();

			// Act
			var result = options.GetPartsTest(table, cols, idColumn);

			// Assert
			var some = result.AssertSome();
			Assert.True(some.SortRandom);
		}

		[Fact]
		public void Adds_Sort()
		{
			// Arrange
			var c0 = Substitute.For<IColumn>();
			var o0 = SortOrder.Ascending;
			var c1 = Substitute.For<IColumn>();
			var o1 = SortOrder.Descending;
			var sort = new[] { (c0, o0), (c1, o1) };
			var (table, _, idColumn, _, options) = QueryOptions_Setup.Get(opt => opt with { Sort = sort });
			var cols = Substitute.For<IColumnList>();

			// Act
			var result = options.GetPartsTest(table, cols, idColumn);

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
