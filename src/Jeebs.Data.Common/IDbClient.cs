// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Data.Common;

namespace Jeebs.Data.Common;

/// <summary>
/// Database client.
/// </summary>
public interface IDbClient : Data.IDbClient
{
	/// <summary>
	/// Executes queries and maps to result types.
	/// </summary>
	IAdapter Adapter { get; }

	/// <summary>
	/// Return an open database connection.
	/// </summary>
	/// <param name="connectionString">Database connection string.</param>
	DbConnection GetConnection(string connectionString);

	/// <summary>
	/// Execute actions requiring type mapping.
	/// </summary>
	/// <remarks>
	/// Implementations must be thread-safe as mapping usually uses static classes.
	/// </remarks>
	/// <param name="mapper">ITypeMapper action.</param>
	void TypeMap(Action<ITypeMapper> mapper);
}
