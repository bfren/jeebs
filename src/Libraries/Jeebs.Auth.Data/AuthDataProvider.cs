// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Models;
using Jeebs.Cryptography;
using static F.OptionF;

namespace Jeebs.Auth
{
	/// <inheritdoc cref="IAuthDataProvider"/>
	public sealed class AuthDataProvider : IAuthDataProvider
	{
		/// <summary>
		/// AuthUserFunc
		/// </summary>
		public AuthUserFunc User { get; private init; }

		/// <summary>
		/// AuthUserFunc
		/// </summary>
		public AuthRoleFunc Role { get; private init; }

		private AuthDb Db { get; init; }

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="db">AuthDb</param>
		/// <param name="log">ILog</param>
		public AuthDataProvider(AuthDb db, ILog<AuthDataProvider> log) =>
			(User, Role, Db) = (new AuthUserFunc(db, log), new AuthRoleFunc(db, log), db);

		/// <inheritdoc cref="ValidateUserAsync{TModel}(string, string)"/>
		public Task<Option<AuthUserModel>> ValidateUserAsync(string email, string password) =>
			ValidateUserAsync<AuthUserModel>(email, password);

		/// <inheritdoc/>
		public async Task<Option<TModel>> ValidateUserAsync<TModel>(string email, string password)
			where TModel : IAuthUserModel
		{
			// Check email
			if (string.IsNullOrEmpty(email))
			{
				return None<TModel, Msg.NullOrEmptyEmailMsg>();
			}

			// Check password
			if (string.IsNullOrEmpty(password))
			{
				return None<TModel, Msg.InvalidPasswordMsg>();
			}

			// Get user for authentication
			foreach (var user in await User.RetrieveAsync(email).ConfigureAwait(false))
			{
				// Verify the user is enabled
				if (!user.IsEnabled)
				{
					return None<TModel>(new Msg.UserNotEnabledMsg(email));
				}

				// Verify the entered password
				if (!user.PasswordHash.VerifyPassword(password))
				{
					return None<TModel, Msg.InvalidPasswordMsg>();
				}

				// Get user model
				return await User.RetrieveAsync<TModel>(user.Id).ConfigureAwait(false);
			}

			// User not found
			return None<TModel>(new Msg.UserNotFoundMsg(email));
		}

		/// <inheritdoc/>
		public Task<Option<bool>> UpdateUserLastSignInAsync(AuthUserId userId) =>
			Db.ExecuteAsync("UpdateUserLastSignIn", new { Id = userId.Value }, CommandType.StoredProcedure);

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Invalid password</summary>
			public sealed record InvalidPasswordMsg : IMsg { }

			/// <summary>Null or empty email address</summary>
			public sealed record NullOrEmptyEmailMsg : IMsg { }

			/// <summary>Null or empty password</summary>
			public sealed record NullOrEmptyPasswordMsg : IMsg { }

			/// <summary>User not enabled</summary>
			public sealed record UserNotEnabledMsg(string Value) : WithValueMsg<string> { }

			/// <summary>User not found</summary>
			public sealed record UserNotFoundMsg(string Value) : WithValueMsg<string> { }
		}
	}
}
