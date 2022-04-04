// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Query;
using Jeebs.WordPress.Tables;
using MaybeF;

namespace Jeebs.WordPress.Query.PostsPartsBuilder_Tests;

public static class Setup
{
	private static readonly WpDbSchema Schema =
		new(Rnd.Str);

	public static PostsTable Post { get; } =
		Schema.Posts;

	public static PostsPartsBuilder GetBuilder(IExtract extract) =>
		new(extract, Schema);

	public static void AssertWhere(QueryParts parts, Maybe<QueryParts> result, string column, Compare cmp, object value)
	{
		var some = result.AssertSome();
		Assert.NotSame(parts, some);
		Assert.Collection(some.Where,
			x =>
			{
				Assert.Equal(Post.GetName(), x.column.TblName);
				Assert.Equal(column, x.column.ColName);
				Assert.Equal(cmp, x.compare);
				Assert.Equal(value, x.value);
			}
		);
	}

	public static IEnumerable<object[]> GetCompareValues()
	{
		yield return new object[] { Compare.Equal };
		yield return new object[] { Compare.NotEqual };
		yield return new object[] { Compare.Like };
		yield return new object[] { Compare.LessThan };
		yield return new object[] { Compare.LessThanOrEqual };
		yield return new object[] { Compare.MoreThan };
		yield return new object[] { Compare.MoreThanOrEqual };
		yield return new object[] { Compare.In };
		yield return new object[] { Compare.NotIn };
	}
}
