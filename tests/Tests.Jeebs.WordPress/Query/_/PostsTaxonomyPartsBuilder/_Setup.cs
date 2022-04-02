// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;

namespace Jeebs.WordPress.Query.PostsTaxonomyPartsBuilder_Tests;

public static class Setup
{
	private static readonly WpDbSchema Schema =
		new(Rnd.Str);

	public static PostsTaxonomyPartsBuilder GetBuilder(IExtract extract) =>
		new(extract, Schema);
}
