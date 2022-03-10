// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;

namespace Jeebs.WordPress.Query.TermsPartsBuilder_Tests;

public static class Setup
{
	private static readonly WpDbSchema schema =
		new(Rnd.Str);

	public static TermsPartsBuilder GetBuilder(IExtract extract) =>
		new(extract, schema);
}
