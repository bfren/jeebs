// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Data;

namespace Jeebs.Auth
{
	/// <inheritdoc cref="IAuthUserFunc{TUser}"/>
	public sealed class AuthUserFunc<TUser> : DbFunc<TUser, AuthUserId>, IAuthUserFunc<TUser>
		where TUser : IAuthUser
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="db">AuthDb</param>
		/// <param name="log">ILog</param>
		public AuthUserFunc(AuthDb db, ILog<AuthUserFunc<TUser>> log) : base(db, log) { }

		/// <inheritdoc/>
		public Task<Option<TUser>> RetrieveAsync(string email)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public Task<Option<bool>> UpdateLastSignInAsync(AuthUserId userId)
		{
			throw new NotImplementedException();
		}
	}
}
