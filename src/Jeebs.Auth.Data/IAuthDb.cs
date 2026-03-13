// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data.Tables;
using Jeebs.Data.Common;

namespace Jeebs.Auth.Data;

/// <summary>
/// Adds additional Auth functionality to the base <see cref="IDb"/>.
/// </summary>
public interface IAuthDb : IDb
{
	/// <summary>
	/// Auth Database Client.
	/// </summary>
	new IAuthDbClient Client { get; }

	/// <summary>
	/// Role Table.
	/// </summary>
	AuthRoleTable Role { get; }

	/// <summary>
	/// User Table.
	/// </summary>
	AuthUserTable User { get; }

	/// <summary>
	/// User Role Table.
	/// </summary>
	AuthUserRoleTable UserRole { get; }

	/// <summary>
	/// Add any custom type handlers.
	/// </summary>
	void AddTypeHandlers();

	/// <summary>
	/// Migrate to the latest version of the Auth database.
	/// </summary>
	void MigrateToLatest();
}
