// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Data;

namespace Jeebs.Auth
{
	/// <summary>
	/// Provides Authentication functions for interacting with Roles
	/// </summary>
	public sealed class AuthRoleFunc : DbFunc<AuthRoleEntity, AuthRoleId>
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="db">AuthDb</param>
		/// <param name="log">ILog</param>
		internal AuthRoleFunc(AuthDb db, ILog log) : base(db, log) { }
	}
}
