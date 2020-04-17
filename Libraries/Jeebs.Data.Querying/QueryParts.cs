using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Query parts
	/// </summary>
	/// <typeparam name="T">Model type</typeparam>
	public sealed class QueryParts<T> : IQueryParts<T>
	{
		/// <summary>
		/// From table
		/// </summary>
		public string? From { get; set; }

		/// <summary>
		/// Select
		/// </summary>
		public string? Select { get; set; }

		/// <summary>
		/// Inner Join
		/// </summary>
		public IList<(string table, string on, string equals)>? InnerJoin { get; set; }

		/// <summary>
		/// Left Join
		/// </summary>
		public IList<(string table, string on, string equals)>? LeftJoin { get; set; }

		/// <summary>
		/// Right Join
		/// </summary>
		public IList<(string table, string on, string equals)>? RightJoin { get; set; }

		/// <summary>
		/// Where
		/// </summary>
		public IList<string>? Where { get; set; }

		/// <summary>
		/// Query Parameters
		/// </summary>
		public IQueryParameters Parameters { get; set; } = new QueryParameters();

		/// <summary>
		/// Order By
		/// </summary>
		public IList<string>? OrderBy { get; set; }

		/// <summary>
		/// Limit
		/// </summary>
		public long? Limit { get; set; }

		/// <summary>
		/// Offset
		/// </summary>
		public long? Offset { get; set; }

		/// <summary>
		/// Only allow internal construction - usually from QueryBuilder
		/// </summary>
		internal QueryParts() { }

		/// <summary>
		/// Get Query
		/// </summary>
		/// <param name="unitOfWork">IUnitOfWork</param>
		public IQuery<T> GetQuery(IUnitOfWork unitOfWork) => new Query<T>(unitOfWork, this);
	}
}
