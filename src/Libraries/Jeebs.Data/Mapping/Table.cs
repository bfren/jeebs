// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Data
{
	/// <summary>
	/// Table
	/// </summary>
	/// <param name="TableName">Table Name</param>
	public abstract record Table(string TableName) : ITable
	{
		/// <summary>
		/// Table name (unescaped)
		/// </summary>
		/// <returns>Table name</returns>
		public override string ToString() =>
			TableName;
	}
}
