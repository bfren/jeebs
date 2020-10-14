using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public static class QueryPartsBuilder
	{
		public static (QueryPartsBuilder<string, Options> builder, IAdapter adapter) GetQueryPartsBuilder()
		{
			var adapter = Substitute.For<IAdapter>();
			var from = F.Rnd.String;
			var builder = Substitute.ForPartsOf<QueryPartsBuilder<string, Options>>(adapter, from);

			return (builder, adapter);
		}

		public class Options : QueryOptions { }
	}
}
