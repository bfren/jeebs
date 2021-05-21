// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data;

namespace Jeebs.WordPress.Data.Query_Tests.PostsPartsBuilder_Tests
{
	public static class Setup
	{
		public static Query.PostsPartsBuilder GetBuilder(IExtract extract)
		{
			var schema = new WpDbSchema(F.Rnd.Str);
			return new(extract, schema);
		}
	}
}
