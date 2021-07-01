// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data;

namespace Jeebs.WordPress.Data.Query_Tests.TermsPartsBuilder_Tests
{
	public static class Setup
	{
		private readonly static WpDbSchema schema =
			new(F.Rnd.Str);

		public static Query.TermsPartsBuilder GetBuilder(IExtract extract) =>
			new(extract, schema);
	}
}
