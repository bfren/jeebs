using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Query
	/// </summary>
	public interface IQuery : IDisposable
	{
		/// <summary>
		/// From
		/// </summary>
		string From { get; }

		/// <summary>
		/// Select
		/// </summary>
		string? Select { get; }

		/// <summary>
		/// Inner Join
		/// </summary>
		List<string>? InnerJoin { get; }

		/// <summary>
		/// Left Join
		/// </summary>
		List<string>? LeftJoin { get; }

		/// <summary>
		/// Right Join
		/// </summary>
		List<string>? RightJoin { get; }

		/// <summary>
		/// Where
		/// </summary>
		List<string>? Where { get; }

		/// <summary>
		/// Order By
		/// </summary>
		List<string>? OrderBy { get; }

		/// <summary>
		/// Limit
		/// </summary>
		double Limit { get; }

		/// <summary>
		/// Offset
		/// </summary>
		public double Offset { get; }

		/// <summary>
		/// Override select value and return IQuery
		/// </summary>
		/// <param name="select">New select</param>
		public IQuery OverrideSelect(string select);

		/// <summary>
		/// Build SELECT query
		/// </summary>
		string GetQuerySql();

		/// <summary>
		/// Build SELECT COUNT query
		/// </summary>
		string GetCountSql();
	}
}
