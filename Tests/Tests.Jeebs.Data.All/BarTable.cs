using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Mapping;

namespace Jeebs.Data
{
	public class BarTable : Table
	{
		public readonly string BarId = "bar_id";

		public BarTable() : base(QueryPartsBuilderExtended_Tests.QueryPartsBuilderExtended.BarTable) { }
	}
}
