// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Tables;
using Xunit;

namespace Jeebs.WordPress.Data.Query_Tests.PostsPartsBuilder_Tests;

public static class Setup
{
	private readonly static WpDbSchema schema =
		new(F.Rnd.Str);

	public static PostTable Post { get; } =
		schema.Post;

	public static Query.PostsPartsBuilder GetBuilder(IExtract extract) =>
		new(extract, schema);

	public static void AssertWhere(QueryParts parts, Maybe<QueryParts> result, string column, Compare cmp, object value)
	{
		var some = result.AssertSome();
		Assert.NotSame(parts, some);
		Assert.Collection(some.Where,
			x =>
			{
				Assert.Equal(Post.GetName(), x.column.TblName);
				Assert.Equal(column, x.column.ColName);
				Assert.Equal(cmp, x.cmp);
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
