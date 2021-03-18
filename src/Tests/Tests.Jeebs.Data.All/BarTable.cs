// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Mapping;

namespace Jeebs.Data
{
	public class BarTable : Table
	{
		public string BarId { get; } = "bar_id";

		public BarTable() : base(QueryPartsBuilderExtended_Tests.QueryPartsBuilderExtended.BarTable) { }
	}
}
