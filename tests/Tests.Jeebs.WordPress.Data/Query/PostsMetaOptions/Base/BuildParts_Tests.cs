// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using Jeebs.Data.Enums;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying.QueryOptions_Tests;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Tables;
using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.Query_Tests.PostsMetaOptions_Tests
{
	public class BuildParts_Tests : BuildParts<Query.PostsMetaOptions, WpPostMetaId>
	{
		private static Lazy<WpDbSchema> Schema = new(() => new(F.Rnd.Str));

		private static PostMetaTable Table =>
			Schema.Value.PostMeta;

		protected override Query.PostsMetaOptions Create(IMapper mapper)
		{
			var db = Substitute.For<IWpDb>();
			db.Schema.Returns(Schema.Value);
			return new(db);
		}

		[Fact]
		public override void Test00_Returns_New_QueryParts_With_Where_Id() =>
			Test00();

		[Fact]
		public override void Test01_Returns_New_QueryParts_With_Where_Ids() =>
			Test01();

		[Fact]
		public override void Test02_Returns_New_QueryParts_With_Sort_Random() =>
			Test02();

		[Fact]
		public override void Test03_Returns_New_QueryParts_With_Sort() =>
			Test03();

		[Fact]
		public void Returns_New_QueryParts_With_Where_PostId()
		{
			// Arrange
			var postId = new WpPostId { Value = F.Rnd.Lng };
			var (options, v) = Setup(opt => opt with { PostId = postId });
			var cols = Substitute.For<IColumnList>();
			var whereCol = new Column(Table.GetName(), Table.PostId, nameof(Table.PostId));

			// Act
			var result = options.BuildPartsTest(v.Table, cols, v.IdColumn);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Collection(some.Where,
				x =>
				{
					Assert.Equal(whereCol, x.column);
					Assert.Equal(Compare.Equal, x.cmp);
					Assert.Equal(postId.Value, x.value);
				}
			);
		}

		[Fact]
		public void Returns_New_QueryParts_With_Where_PostIds()
		{
			// Arrange
			var i0 = new WpPostId { Value = F.Rnd.Lng };
			var i1 = new WpPostId { Value = F.Rnd.Lng };
			var (options, v) = Setup(opt => opt with { PostIds = new[] { i0, i1 }.ToImmutableList() });
			var cols = Substitute.For<IColumnList>();
			var whereCol = new Column(Table.GetName(), Table.PostId, nameof(Table.PostId));

			// Act
			var result = options.BuildPartsTest(v.Table, cols, v.IdColumn);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Collection(some.Where,
				x =>
				{
					Assert.Equal(whereCol, x.column);
					Assert.Equal(Compare.In, x.cmp);

					var value = Assert.IsAssignableFrom<IEnumerable<long>>(x.value);
					Assert.Collection(value,
						y => Assert.Equal(i0.Value, y),
						y => Assert.Equal(i1.Value, y)
					);
				}
			);
		}

		[Fact]
		public void Returns_New_QueryParts_With_Where_Key()
		{
			// Arrange

			// Act

			// Assert
		}
	}
}
