// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Data.Mapping;
using Jeebs.Data.Querying;
using Jeebs.WordPress.Data.Tables;
using NSubstitute;

namespace Jeebs.WordPress.Data.Query_Tests.PostsMetaOptions_Tests
{
	public abstract class PostsMetaOptions_Tests
	{
		public static (Query.PostsMetaOptions options, Vars v) Setup(Func<Query.PostsMetaOptions, Query.PostsMetaOptions>? opt = null)
		{
			var schema = new WpDbSchema(F.Rnd.Str);

			var idColumn = new Column(schema.PostMeta.GetName(), schema.PostMeta.PostId, nameof(PostMetaTable.PostId));

			var wpDb = Substitute.For<IWpDb>();
			wpDb.Schema.Returns(schema);

			var options = new Query.PostsMetaOptions(wpDb);

			var parts = new QueryParts(schema.PostMeta);

			return (opt?.Invoke(options) ?? options, new(wpDb, idColumn, parts));
		}

		public sealed record Vars(
			IWpDb WpDb,
			IColumn PostIdColumn,
			QueryParts Parts
		);
	}
}
