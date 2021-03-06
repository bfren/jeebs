// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

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
