﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Cryptography;
using static F.OptionF;

namespace Jeebs.Auth
{
	/// <inheritdoc cref="IAuthDataProvider{TUserEntity, TRoleEntity}"/>
	public sealed class AuthDataProvider : IAuthDataProvider<AuthUserEntity, AuthRoleEntity>
	{
		/// <inheritdoc/>
		public IAuthUserFunc<AuthUserEntity> User { get; private init; }

		/// <inheritdoc/>
		public IAuthRoleFunc<AuthRoleEntity> Role { get; private init; }

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="user">IAuthUserFunc</param>
		/// <param name="role">IAuthRoleFunc</param>
		public AuthDataProvider(IAuthUserFunc<AuthUserEntity> user, IAuthRoleFunc<AuthRoleEntity> role) =>
			(User, Role) = (user, role);

		/// <inheritdoc/>
		public async Task<Option<TModel>> ValidateUserAsync<TModel>(string email, string password)
			where TModel : IAuthUser
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
			foreach (var user in await User.RetrieveAsync<AuthUserEntity>(email).ConfigureAwait(false))
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
			public sealed record UserNotFoundMsg(string EmailAddress) : NotFoundMsg { }
		}
	}
}