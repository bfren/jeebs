// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Data.Mapping
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