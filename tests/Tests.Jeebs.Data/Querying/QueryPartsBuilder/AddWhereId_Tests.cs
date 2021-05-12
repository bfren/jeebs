// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Jeebs.Data.Enums;
using Xunit;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public class AddWhereId_Tests : QueryPartsBuilder_Tests
	{
		[Fact]
		public void Id_And_Ids_Null_Returns_Original_Parts()
		{
			// Arrange
			var (builder, v) = Setup();

			// Act
			var result = builder.AddWhereId(v.Parts, v.IdColumn, null, ImmutableList.Empty<TestId>());

			// Assert
			var some = result.AssertSome();
			Assert.Same(v.Parts, some);
		}

		[Fact]
		public void Id_Set_Adds_Where_Id_Equal()
		{
			// Arrange
			var id = new TestId(F.Rnd.Lng);
			var (builder, v) = Setup();

			// Act
			var result = builder.AddWhereId(v.Parts, v.IdColumn, id, ImmutableList.Empty<TestId>());

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Collection(some.Where,
				x =>
				{
					Assert.Same(v.IdColumn, x.column);
					Assert.Equal(Compare.Equal, x.cmp);
					Assert.Equal(id.Value, x.value);
				}
			);
		}

		[Fact]
		public void Id_And_Ids_Set_Adds_Where_Id_Equal()
		{
			// Arrange
			var i0 = new TestId(F.Rnd.Lng);
			var i1 = new TestId(F.Rnd.Lng);
			var i2 = new TestId(F.Rnd.Lng);
			var ids = ImmutableList.Create(i1, i2);
			var (builder, v) = Setup();

			// Act
			var result = builder.AddWhereId(v.Parts, v.IdColumn, i0, ids);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Collection(some.Where,
				x =>
				{
					Assert.Same(v.IdColumn, x.column);
					Assert.Equal(Compare.Equal, x.cmp);
					Assert.Equal(i0.Value, x.value);
				}
			);
		}

		[Fact]
		public void Id_Null_Ids_Set_Adds_Where_Id_In()
		{
			// Arrange
			var i0 = new TestId(F.Rnd.Lng);
			var i1 = new TestId(F.Rnd.Lng);
			var ids = ImmutableList.Create(i0, i1);
			var (options, v) = Setup();

			// Act
			var result = options.AddWhereId(v.Parts, v.IdColumn, null, ids);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
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
	}
}
