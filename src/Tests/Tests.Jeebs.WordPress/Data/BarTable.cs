// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data.Mapping;

namespace Jeebs.WordPress.Data
{
	public class BarTable : Table
	{
		public string BarId { get; } = "bar_id";

		public BarTable() : base(QueryPartsBuilderExtended_Tests.QueryPartsBuilderExtended.BarTable) { }
	}
}
