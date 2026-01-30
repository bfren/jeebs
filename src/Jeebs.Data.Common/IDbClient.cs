// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data.Common;

namespace Jeebs.Data.Common;

/// <summary>
/// Database client.
/// </summary>
public interface IDbClient : Data.IDbClient
{

	/// <summary>
	/// Type mapper.
	/// </summary>
	IDbTypeMapper Types { get; }

	/// <summary>
	/// Return an open database connection.
	/// </summary>
	/// <param name="connectionString">Database connection string.</param>
	DbConnection GetConnection(string connectionString);
}
