// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Tables;
using Jeebs.Config;
using Jeebs.Data;
using Microsoft.Extensions.Options;

namespace Jeebs.Auth
{
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
		/// <param name="config">DbConfig</param>
		/// <param name="log">ILog</param>
		/// <param name="client">IAuthDbClient</param>
		public AuthDb(IOptions<DbConfig> config, ILog<AuthDb> log, IAuthDbClient client) :
			base(config, log, client, config.Value.Authentication)
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
		public void MigrateToLatest() =>
			Client.MigrateToLatest(Config.ConnectionString);

		/// <summary>
		/// Add type handlers
		/// </summary>
		static AuthDb()
		{
			AddStrongIdTypeHandler<AuthRoleId>();
			AddStrongIdTypeHandler<AuthUserId>();
			AddStrongIdTypeHandler<AuthUserRoleId>();
		}
	}
}
