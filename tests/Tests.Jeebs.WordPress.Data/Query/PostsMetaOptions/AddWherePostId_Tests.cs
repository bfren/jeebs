// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Jeebs.Data.Enums;
using Jeebs.WordPress.Data.Entities;
using Xunit;

namespace Jeebs.WordPress.Data.Query_Tests.PostsMetaOptions_Tests
{
	public class AddWherePostId_Tests : PostsMetaOptions_Tests
	{
		[Fact]
		public void PostId_And_PostIds_Null_Returns_Original_Parts()
		{
			// Arrange
			var (options, v) = Setup();

			// Act
			var result = options.AddWherePostId(v.Parts);

			// Assert
			var some = result.AssertSome();
			Assert.Same(v.Parts, some);
		}

		[Fact]
		public void PostId_Set_Adds_Where_PostId_Equal()
		{
			// Arrange
			var id = F.Rnd.Lng;
			var (options, v) = Setup(opt => opt with { PostId = new(id) });

			// Act
			var result = options.AddWherePostId(v.Parts);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Collection(some.Where,
				x =>
				{
					Assert.Equal(v.PostIdColumn, x.column);
					Assert.Equal(Compare.Equal, x.cmp);
					Assert.Equal(id, x.value);
				}
			);
		}

		[Fact]
		public void PostId_And_PostIds_Set_Adds_Where_PostId_Equal()
		{
			// Arrange
			var i0 = F.Rnd.Lng;
			var i1 = F.Rnd.Lng;
			var i2 = F.Rnd.Lng;
			var ids = ImmutableList.Create<WpPostId>(new(i0), new(i1), new(i2));
			var (options, v) = Setup(opt => opt with { PostId = new(i0), PostIds = ids });

			// Act
			var result = options.AddWherePostId(v.Parts);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Collection(some.Where,
				x =>
				{
					Assert.Equal(v.PostIdColumn, x.column);
					Assert.Equal(Compare.Equal, x.cmp);
					Assert.Equal(i0, x.value);
				}
			);
		}

		[Fact]
		public void PostId_Null_PostIds_Set_Adds_Where_PostId_In()
		{
			// Arrange
			var i0 = F.Rnd.Lng;
			var i1 = F.Rnd.Lng;
			var ids = ImmutableList.Create<WpPostId>(new(i0), new(i1));
			var (options, v) = Setup(opt => opt with { PostIds = ids });

			// Act
			var result = options.AddWherePostId(v.Parts);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Collection(some.Where,
				x =>
				{
					Assert.Equal(v.PostIdColumn, x.column);
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
