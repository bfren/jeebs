// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Tables;
using Jeebs.Config;
using Jeebs.Data;
using Microsoft.Extensions.Options;

namespace Jeebs.Auth
{
	/// <summary>
	/// Authentication Database wrapper
	/// </summary>
	public sealed class AuthDb : Db
	{
		/// <summary>
		/// Role Table
		/// </summary>
		public RoleTable Role { get; } = new();

		/// <summary>
		/// User Table
		/// </summary>
		public UserTable User { get; } = new();

		/// <summary>
		/// User Role Table
		/// </summary>
		public UserRoleTable UserRole { get; } = new();

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="client">IDbClient</param>
		/// <param name="logs">DbLogs</param>
		/// <param name="config">DbConfig</param>
		/// <param name="connectionName">Authentication connection name</param>
		public AuthDb(IOptions<DbConfig> config, ILog<AuthDb> log, IDbClient client, string connectionName) :
			base(config, log, client, connectionName)
		{
			// Map entities to tables
			Map<RoleEntity>.To(Role);
			Map<UserEntity>.To(User);
			Map<UserRoleEntity>.To(UserRole);
		}
	}
}
