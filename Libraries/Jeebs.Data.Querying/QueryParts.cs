using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Querying
{
	/// <inheritdoc cref="IQueryParts"/>
	public sealed class QueryParts : IQueryParts
	{
		/// <inheritdoc/>
		public string From { get; }

		/// <inheritdoc/>
		public string? Select { get; set; }

		/// <inheritdoc/>
		public IList<(string table, string on, string equals)>? InnerJoin { get; set; }

		/// <inheritdoc/>
		public IList<(string table, string on, string equals)>? LeftJoin { get; set; }

		/// <inheritdoc/>
		public IList<(string table, string on, string equals)>? RightJoin { get; set; }

		/// <inheritdoc/>
		public IList<string>? Where { get; set; }

		/// <inheritdoc/>
		public IQueryParameters Parameters { get; set; } = new QueryParameters();

		/// <inheritdoc/>
		public IList<string>? OrderBy { get; set; }

		/// <inheritdoc/>
		public long? Limit { get; set; }

		/// <inheritdoc/>
		public long? Offset { get; set; }

		/// <summary>
		/// Only allow internal construction - usually from QueryBuilder
		/// </summary>
		internal QueryParts(string from)
			=> From = from;
	}
}
