// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryOptions_Tests
{
	public class AddWhereId_Tests
	{
		[Fact]
		public void Id_And_Ids_Null_Returns_Original_Parts()
		{
			// Arrange
			var (_, _, idColumn, parts, options) = QueryOptions_Setup.Get();

			// Act
			var result = options.AddWhereIdTest(parts, idColumn);

			// Assert
			var some = result.AssertSome();
			Assert.Same(parts, some);
		}

		[Fact]
		public void Id_Set_Adds_Where_Id_Equal()
		{
			// Arrange
			var id = F.Rnd.Lng;
			var (_, _, idColumn, parts, options) = QueryOptions_Setup.Get(opt => opt with { Id = new(id) });

			// Act
			var result = options.AddWhereIdTest(parts, idColumn);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(parts, some);
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
		public void Id_And_Ids_Set_Adds_Where_Id_Equal()
		{
			// Arrange
			var i0 = F.Rnd.Lng;
			var i1 = F.Rnd.Lng;
			var i2 = F.Rnd.Lng;
			var ids = new QueryOptions_Setup.TestId[] { new(i1), new(i2) };
			var (_, _, idColumn, parts, options) = QueryOptions_Setup.Get(opt => opt with { Id = new(i0), Ids = ids });

			// Act
			var result = options.AddWhereIdTest(parts, idColumn);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(parts, some);
			Assert.Collection(some.Where,
				x =>
				{
					Assert.Same(idColumn, x.column);
					Assert.Equal(Compare.Equal, x.cmp);
					Assert.Equal(i0, x.value);
				}
			);
		}

		[Fact]
		public void Id_Null_Ids_Set_Adds_Where_Id_In()
		{
			// Arrange
			var i0 = F.Rnd.Lng;
			var i1 = F.Rnd.Lng;
			var ids = new QueryOptions_Setup.TestId[] { new(i0), new(i1) };
			var (_, _, idColumn, parts, options) = QueryOptions_Setup.Get(opt => opt with { Ids = ids });

			// Act
			var result = options.AddWhereIdTest(parts, idColumn);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(parts, some);
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
	}
}
