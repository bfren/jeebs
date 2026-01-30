// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data;

/// <summary>
/// Table interface.
/// </summary>
public interface ITable
{
	/// <summary>
	/// Get Table Name.
	/// </summary>
	IDbName GetName();
}
