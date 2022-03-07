// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;

namespace Jeebs.WordPress.Data.Query_Tests.PostsTaxonomyPartsBuilder_Tests;

public static class Setup
{
	private static readonly WpDbSchema schema =
		new(F.Rnd.Str);

	public static Query.PostsTaxonomyPartsBuilder GetBuilder(IExtract extract) =>
		new(extract, schema);
}
