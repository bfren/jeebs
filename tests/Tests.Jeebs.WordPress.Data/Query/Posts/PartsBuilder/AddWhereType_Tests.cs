// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying.QueryPartsBuilder_Tests;
using Jeebs.WordPress.Data.Entities;
using Jeebs.WordPress.Data.Enums;
using Xunit;
using static Jeebs.WordPress.Data.Query_Tests.PostsPartsBuilder_Tests.Setup;

namespace Jeebs.WordPress.Data.Query_Tests.PostsPartsBuilder_Tests
{
	public class AddWhereType_Tests : QueryPartsBuilder_Tests<Query.PostsPartsBuilder, WpPostId>
	{
		protected override Query.PostsPartsBuilder GetConfiguredBuilder(IExtract extract) =>
			GetBuilder(extract);

		[Fact]
		public void Adds_Type_To_Where()
		{
			// Arrange
			var (builder, v) = Setup();
			var type = new PostType(F.Rnd.Str);

			// Act
			var result = builder.AddWhereType(v.Parts, type);

			// Assert
			var some = result.AssertSome();
			Assert.NotSame(v.Parts, some);
			Assert.Collection(some.Where,
				x =>
				{
					Assert.Equal(builder.TTest.Post.GetName(), x.column.Table);
					Assert.Equal(builder.TTest.Post.Type, x.column.Name);
					Assert.Equal(Compare.Equal, x.cmp);
					Assert.Equal(type, x.value);
				}
			);
		}
	}
}
