// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Query;
using Jeebs.WordPress.Tables;
using MaybeF;

namespace Jeebs.WordPress.Query.PostsMetaPartsBuilder_Tests;

public static class Setup
{
	private static readonly WpDbSchema Schema =
		new(Rnd.Str);

	public static PostsTable Posts { get; } =
		Schema.Posts;

	public static PostsMetaTable PostsMeta { get; } =
		Schema.PostsMeta;

	public static PostsMetaPartsBuilder GetBuilder(IExtract extract) =>
		new(extract, Schema);

	public static void AssertWhere(QueryParts parts, Maybe<QueryParts> result, string column, Compare cmp, object value)
	{
		var some = result.AssertSome();
		Assert.NotSame(parts, some);
		Assert.Collection(some.Where,
			x =>
			{
				Assert.Equal(PostsMeta.GetName(), x.column.TblName);
				Assert.Equal(column, x.column.ColName);
				Assert.Equal(cmp, x.cmp);
				Assert.Equal(value, x.value);
			}
		);
	}
}
