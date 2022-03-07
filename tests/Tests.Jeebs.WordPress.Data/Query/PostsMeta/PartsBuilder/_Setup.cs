// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Tables;
using Xunit;

namespace Jeebs.WordPress.Data.Query_Tests.PostsMetaPartsBuilder_Tests;

public static class Setup
{
	private static readonly WpDbSchema schema =
		new(F.Rnd.Str);

	public static PostTable Post { get; } =
		schema.Post;

	public static PostMetaTable PostMeta { get; } =
		schema.PostMeta;

	public static Query.PostsMetaPartsBuilder GetBuilder(IExtract extract) =>
		new(extract, schema);

	public static void AssertWhere(QueryParts parts, Maybe<QueryParts> result, string column, Compare cmp, object value)
	{
		var some = result.AssertSome();
		Assert.NotSame(parts, some);
		Assert.Collection(some.Where,
			x =>
			{
				Assert.Equal(PostMeta.GetName(), x.column.TblName);
				Assert.Equal(column, x.column.ColName);
				Assert.Equal(cmp, x.cmp);
				Assert.Equal(value, x.value);
			}
		);
	}
}
