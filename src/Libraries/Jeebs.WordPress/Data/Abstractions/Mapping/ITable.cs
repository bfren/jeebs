// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Mapping
{
	/// <summary>
	/// Table
	/// </summary>
	public interface ITable
	{
		/// <summary>
		/// Should be overridden to provide the table name (unescaped)
		/// </summary>
		string ToString();
	}
}