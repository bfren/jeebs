// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Models;
using Jeebs.Cryptography;
using Jeebs.Data;
using static F.OptionF;

namespace Jeebs.Auth
{
	/// <inheritdoc cref="IAuthDataProvider"/>
	public sealed class AuthDataProvider : IAuthDataProvider
	{
		/// <summary>
		/// AuthUserFunc
		/// </summary>
		private AuthUserFunc User { get; init; }

		/// <summary>
		/// ILog
		/// </summary>
		private ILog Log { get; init; }

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="db">AuthDb</param>
		/// <param name="log">ILog</param>
		public AuthDataProvider(AuthDb db, ILog<AuthDataProvider> log) =>
			(User, Log) = (new AuthUserFunc(db, log), log);

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

				// Update last sign in
				var updated = await User.UpdateLastSignInAsync(user.Id).ConfigureAwait(false);
				updated.AuditSwitch(none: r => Log.Message(r));

				// Get user model
				return await User.RetrieveAsync<TModel>(user.Id).ConfigureAwait(false);
			}

			// User not found
			return None<TModel>(new Msg.UserNotFoundMsg(email));
		}

		/// <inheritdoc/>
		public Task<Option<AuthUserId>> CreateUserAsync<TModel>(TModel user)
			where TModel : IAuthCreateUserModel
		{
			// Create entity from model
			var entity = new AuthUserEntity(user.Password.HashPassword())
			{
				EmailAddress = user.EmailAddress,
				IsEnabled = true
			};

			// Insert entity and return
			return User.CreateAsync(entity);
		}

		/// <inheritdoc/>
		public Task<Option<AuthUserModel>> RetrieveUserAsync(AuthUserId id) =>
			RetrieveUserAsync<AuthUserModel>(id);

		/// <inheritdoc/>
		public Task<Option<TModel>> RetrieveUserAsync<TModel>(AuthUserId id) =>
			User.RetrieveAsync<TModel>(id);

		/// <inheritdoc/>
		public Task<Option<bool>> UpdateUserAsync(AuthUserModel user) =>
			UpdateUserAsync<AuthUserModel>(user);

		/// <inheritdoc/>
		public Task<Option<bool>> UpdateUserAsync<TModel>(TModel user)
			where TModel : IWithId =>
			User.UpdateAsync(user);

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
