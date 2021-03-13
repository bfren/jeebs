// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Cryptography;
using static F.OptionF;
using Msg = Jeebs.Auth.DataAuthProviderMsg;

namespace Jeebs.Auth
{
	/// <inheritdoc cref="IDataAuthProvider{TUser}"/>
	public abstract class DataAuthProvider<TUser> : IDataAuthProvider<TUser>
		where TUser : IAuthUser, IUserModel
	{
		protected IAuthDb Db { get; private init; }

		protected ILog Log { get; private init; }

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="db">IAuthDb</param>
		/// <param name="log">ILog</param>
		protected DataAuthProvider(IAuthDb db, ILog log) =>
			(Db, Log) = (db, log);

		/// <inheritdoc/>
		public async Task<Option<TUser>> ValidateUserAsync(string email, string password)
		{
			// Check email
			if (string.IsNullOrEmpty(email))
			{
				return None<TUser, Msg.NullOrEmptyEmailMsg>();
			}

			// Check password
			if (string.IsNullOrEmpty(password))
			{
				return None<TUser, Msg.InvalidPasswordMsg>();
			}

			// Get user for authentication and verify password
			foreach (var user in await Db.GetAuthUserAsync<TUser>(email).ConfigureAwait(false))
			{
				if (user.PasswordHash.VerifyPassword(password))
				{
					// Update last sign in
					var updated = await Db.UpdateUserLastSignIn(user.UserId).ConfigureAwait(false);
					updated.AuditSwitch(none: r => Log.Message(r));

					// Return user model
					return await Db.GetUserAsync<TUser>(user.UserId).ConfigureAwait(false);
				}

				// Password not verified
				return None<TUser, Msg.InvalidPasswordMsg>();
			}

			// User not found
			return None<TUser>(new Msg.UserNotFoundMsg(email));
		}
	}

	/// <inheritdoc cref="IDataAuthProvider{TUser,TRole}"/>
	public abstract class DataAuthProvider<TUser, TRole> : DataAuthProvider<TUser>, IDataAuthProvider<TUser, TRole>
		where TUser : IAuthUser, IUserModel<TRole>
		where TRole : IRoleModel
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="db">IAuthDb</param>
		/// <param name="log">ILog</param>
		protected DataAuthProvider(IAuthDb db, ILog log) : base(db, log) { }

		/// <inheritdoc/>
		new public Task<Option<TUser>> ValidateUserAsync(string email, string password) =>
			base.ValidateUserAsync(email, password);
	}

	namespace DataAuthProviderMsg
	{
		/// <summary>Invalid password</summary>
		public sealed record InvalidPasswordMsg : IMsg { }

		/// <summary>Null or empty email address</summary>
		public sealed record NullOrEmptyEmailMsg : IMsg { }

		/// <summary>Null or empty password</summary>
		public sealed record NullOrEmptyPasswordMsg : IMsg { }

		/// <summary>User not found</summary>
		public sealed record UserNotFoundMsg(string Value) : WithValueMsg<string> { }
	}
}
