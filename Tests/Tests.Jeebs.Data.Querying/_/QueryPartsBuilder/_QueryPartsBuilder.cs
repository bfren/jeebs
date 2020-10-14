using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public static class QueryPartsBuilder
	{
		public static (Builder builder, IAdapter adapter) GetQueryPartsBuilder()
		{
			var adapter = Substitute.For<IAdapter>();
			var from = F.Rnd.String;
			var builder = new Builder(adapter, from);

			return (builder, adapter);
		}

		public class Builder : QueryPartsBuilder<string, Options>
		{
			public Builder(IAdapter adapter, string from) : base(adapter, from) { }

			public override IQueryParts Build(Options opt)
			{
				throw new NotImplementedException();
			}

			new public void AddSelect(string select, bool overwrite = false)
				=> base.AddSelect(select, overwrite);
		}

		public class Options : QueryOptions { }
	}
}
