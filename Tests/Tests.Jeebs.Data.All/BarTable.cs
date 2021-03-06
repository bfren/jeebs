using Jeebs.Data.Mapping;

namespace Jeebs.Data
{
	public class BarTable : Table
	{
		public string BarId { get; } = "bar_id";

		public BarTable() : base(QueryPartsBuilderExtended_Tests.QueryPartsBuilderExtended.BarTable) { }
	}
}
