// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Tables;
using Jeebs.Config;
using Jeebs.Data;
using Microsoft.Extensions.Options;

namespace Jeebs.Auth;

/// <inheritdoc cref="IAuthDb"/>
public sealed class AuthDb : Db, IAuthDb
{
	/// <inheritdoc/>
	new public IAuthDbClient Client { get; private init; }

	/// <summary>
	/// Role Table
	/// </summary>
	public AuthRoleTable Role { get; }

	/// <summary>
	/// User Table
	/// </summary>
	public AuthUserTable User { get; }

	/// <summary>
	/// User Role Table
	/// </summary>
	public AuthUserRoleTable UserRole { get; }

	/// <summary>
	/// Create object
	/// </summary>
	/// <param name="client">IAuthDbClient</param>
	/// <param name="config">DbConfig</param>
	/// <param name="log">ILog</param>
	public AuthDb(IAuthDbClient client, IOptions<DbConfig> config, ILog<AuthDb> log) :
		base(client, config, log, config.Value.Authentication)
	{
		// Set Client
		Client = client;

		// Create tables
		Role = new();
		User = new();
		UserRole = new();

		// Map entities to tables
		Map<AuthRoleEntity>.To(Role);
		Map<AuthUserEntity>.To(User);
		Map<AuthUserRoleEntity>.To(UserRole);
	}

	/// <inheritdoc/>
	protected override void AddTypeHandlers(DbMapper mapper) =>
		mapper.AddStrongIdTypeHandlers();

	/// <inheritdoc/>
	public void MigrateToLatest() =>
		Client.MigrateToLatest(Config.ConnectionString);
}
