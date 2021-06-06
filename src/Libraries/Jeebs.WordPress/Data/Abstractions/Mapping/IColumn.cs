// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Mapping
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
