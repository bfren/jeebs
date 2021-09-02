﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Enums;
using Xunit;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public abstract class AddWhereId_Tests<TBuilder, TId> : QueryPartsBuilder_Tests<TBuilder, TId>
		where TBuilder : QueryPartsBuilder<TId>
		where TId : IStrongId, new()
	{
		public abstract void Test00_Id_And_Ids_Null_Returns_Original_Parts();

		protected void Test00()
		{
			// Arrange
			var (builder, v) = Setup();

			// Act
			var result = builder.AddWhereId(v.Parts, default, ImmutableList.Empty<TId>());

			// Assert
			var some = result.AssertSome();
			Assert.Same(v.Parts, some);
		}

		public abstract void Test01_Id_Set_Adds_Where_Id_Equal();

		protected void Test01()
		{
			// Arrange
			var id = new TId { Value = F.Rnd.Ulng };
			var (builder, v) = Setup();

			// Act
			var result = builder.AddWhereId(v.Parts, id, ImmutableList.Empty<TId>());

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Collection(some.Where,
				x =>
				{
					Assert.Equal(builder.IdColumn, x.column);
					Assert.Equal(Compare.Equal, x.cmp);
					Assert.Equal(id.Value, x.value);
				}
			);
		}

		public abstract void Test02_Id_And_Ids_Set_Adds_Where_Id_Equal();

		protected void Test02()
		{
			// Arrange
			var i0 = new TId { Value = F.Rnd.Ulng };
			var i1 = new TId { Value = F.Rnd.Ulng };
			var i2 = new TId { Value = F.Rnd.Ulng };
			var ids = ImmutableList.Create(i1, i2);
			var (builder, v) = Setup();

			// Act
			var result = builder.AddWhereId(v.Parts, i0, ids);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Collection(some.Where,
				x =>
				{
					Assert.Equal(builder.IdColumn, x.column);
					Assert.Equal(Compare.Equal, x.cmp);
					Assert.Equal(i0.Value, x.value);
				}
			);
		}

		public abstract void Test03_Id_Null_Ids_Set_Adds_Where_Id_In();

		protected void Test03()
		{
			// Arrange
			var i0 = new TId { Value = F.Rnd.Ulng };
			var i1 = new TId { Value = F.Rnd.Ulng };
			var ids = ImmutableList.Create(i0, i1);
			var (builder, v) = Setup();

			// Act
			var result = builder.AddWhereId(v.Parts, default, ids);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Collection(some.Where,
				x =>
				{
					Assert.Equal(builder.IdColumn, x.column);
					Assert.Equal(Compare.In, x.cmp);

					var value = Assert.IsAssignableFrom<IEnumerable<ulong>>(x.value);
					Assert.Collection(value,
						y => Assert.Equal(i0.Value, y),
						y => Assert.Equal(i1.Value, y)
					);
				}
			);
		}
	}
}
