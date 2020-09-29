using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping
{
	/// <summary>
	/// Holds information about a column
	/// </summary>
	public interface IColumn
	{
		/// <summary>
		/// Table Name
		/// </summary>
		string Table { get; }

		/// <summary>
		/// Column Name
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Column Alias
		/// </summary>
		string Alias { get; }
	}
}
