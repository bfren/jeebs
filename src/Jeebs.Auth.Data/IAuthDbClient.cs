// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;

namespace Jeebs.Auth.Data;

/// <summary>
/// Adds additional Authentication functionality to the base <see cref="IDbClient"/>.
/// </summary>
public interface IAuthDbClient : IDbClient
{
	/// <summary>
	/// Returns a query that updates a user's last sign in.
	/// </summary>
	string GetUpdateUserLastSignInQuery();

	/// <summary>
	/// Nuke database (i.e. migrate to 0.
	/// </summary>
	/// <param name="connectionString">Connection string</param>
	void Nuke(string connectionString);

	/// <summary>
	/// Migrate to the latest version of the Authentication database.
	/// </summary>
	/// <param name="connectionString">Connection string</param>
	void MigrateToLatest(string connectionString);
}
