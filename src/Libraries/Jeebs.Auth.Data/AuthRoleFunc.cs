// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Data;

namespace Jeebs.Auth
{
	/// <inheritdoc cref="IAuthRoleFunc{TRoleEntity}"/>
	public sealed class AuthRoleFunc : DbFunc<AuthRoleEntity, AuthRoleId>, IAuthRoleFunc<AuthRoleEntity>
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="db">IAuthDb</param>
		/// <param name="log">ILog</param>
		public AuthRoleFunc(IAuthDb db, ILog<AuthRoleFunc> log) : base(db, log) { }
	}
}
