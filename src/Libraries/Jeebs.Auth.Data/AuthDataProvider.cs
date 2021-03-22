// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Cryptography;
using static F.OptionF;

namespace Jeebs.Auth
{
	/// <inheritdoc cref="IAuthDataProvider{TUser}"/>
	public abstract class AuthDataProvider<TUser> : IAuthDataProvider<TUser>
		where TUser : IAuthUser
	{
		/// <summary>
		/// IAuthDb
		/// </summary>
		protected IAuthUserFunc<TUser> User { get; private init; }

		/// <summary>
		/// ILog
		/// </summary>
		protected ILog Log { get; private init; }

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="user">IAuthUserCrud</param>
		/// <param name="log">ILog</param>
		protected AuthDataProvider(IAuthUserFunc<TUser> user, ILog log) =>
			(User, Log) = (user, log);

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
			foreach (var user in await User.RetrieveAsync(email).ConfigureAwait(false))
			{
				if (user.PasswordHash.VerifyPassword(password))
				{
					// Update last sign in
					var updated = await User.UpdateLastSignInAsync(user.UserId).ConfigureAwait(false);
					updated.AuditSwitch(none: r => Log.Message(r));

					// Return user model
					return await User.RetrieveAsync<TUser>(user.UserId).ConfigureAwait(false);
				}

				// Password not verified
				return None<TUser, Msg.InvalidPasswordMsg>();
			}

			// User not found
			return None<TUser>(new Msg.UserNotFoundMsg(email));
		}

		/// <summary>Messages</summary>
		public static class Msg
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

	/// <inheritdoc cref="IDataAuthProvider{TUser,TRole}"/>
	public abstract class DataAuthProvider<TUser, TRole> : AuthDataProvider<TUser>, IDataAuthProvider<TUser, TRole>
		where TUser : IAuthUser<TRole>
		where TRole : IAuthRole
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="user">IAuthUserCrud</param>
		/// <param name="log">ILog</param>
		protected DataAuthProvider(IAuthUserFunc<TUser> user, ILog log) : base(user, log) { }

		/// <inheritdoc/>
		new public Task<Option<TUser>> ValidateUserAsync(string email, string password) =>
			base.ValidateUserAsync(email, password);
	}
}
