using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	/// <summary>
	/// Holds information about a column extracted from a Model for the select clause of a query
	/// </summary>
	public interface IExtractedColumn
	{
		/// <summary>
		/// Escaped Table Name
		/// </summary>
		string Table { get; }

		/// <summary>
		/// Escaped Column Name
		/// </summary>
		string Column { get; }

		/// <summary>
		/// Column Alias
		/// </summary>
		string Alias { get; }
	}
}
