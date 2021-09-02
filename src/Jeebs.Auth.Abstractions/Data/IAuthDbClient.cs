// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;

namespace Jeebs.Auth.Data;

/// <summary>
/// Adds additional Authentication functionality to the base <see cref="IDbClient"/>
/// </summary>
public interface IAuthDbClient : IDbClient
{
	/// <summary>
	/// Migrate to the latest version of the Authentication database
	/// </summary>
	/// <param name="connectionString">Connection string</param>
	void MigrateToLatest(string connectionString);
}
