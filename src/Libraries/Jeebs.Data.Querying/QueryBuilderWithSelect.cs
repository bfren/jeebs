// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using static F.OptionF;

namespace Jeebs.Data.Querying
{
	/// <summary>
	/// Once <see cref="QueryBuilderWithFrom"/> has defined columns to select, the query parts can be retrieved
	/// </summary>
	public record QueryBuilderWithSelect
	{
		/// <summary>
		/// Query parts
		/// </summary>
		private QueryParts Parts { get; init; }

		/// <summary>
		/// Inject query parts
		/// </summary>
		/// <param name="parts">Query parts</param>
		internal QueryBuilderWithSelect(QueryParts parts) =>
			Parts = parts;

		/// <inheritdoc/>
		public Option<IQueryParts> GetParts()
		{
			// Verify parts

			// Return
			return Return((IQueryParts)Parts);
		}
	}
}
