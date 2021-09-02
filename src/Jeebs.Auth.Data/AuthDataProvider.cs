// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Cryptography;
using Jeebs.Linq;
using static F.OptionF;

namespace Jeebs.Auth
{
	/// <inheritdoc cref="IAuthDataProvider{TUserEntity, TRoleEntity, TUserRoleEntity}"/>
	public interface IAuthDataProvider : IAuthDataProvider<AuthUserEntity, AuthRoleEntity, AuthUserRoleEntity>
	{ }

	/// <inheritdoc cref="IAuthDataProvider{TUserEntity, TRoleEntity, TUserRoleEntity}"/>
	public sealed class AuthDataProvider : IAuthDataProvider
	{
		/// <inheritdoc/>
		public IAuthUserRepository<AuthUserEntity> User { get; private init; }

		/// <inheritdoc/>
		public IAuthRoleRepository<AuthRoleEntity> Role { get; private init; }

		/// <inheritdoc/>
		public IAuthUserRoleRepository<AuthUserRoleEntity> UserRole { get; private init; }

		/// <inheritdoc/>
		public IAuthDbQuery Query { get; private init; }

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="user">IAuthUserRepository</param>
		/// <param name="role">IAuthRoleRepository</param>
		/// <param name="userRole">IAuthUserRoleRepository</param>
		/// <param name="query">IAuthDbQuery</param>
		public AuthDataProvider(
			IAuthUserRepository user,
			IAuthRoleRepository role,
			IAuthUserRoleRepository userRole,
			IAuthDbQuery query
		) =>
			(User, Role, UserRole, Query) = (user, role, userRole, query);

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

		/// <inheritdoc/>
		public Task<Option<TUser>> RetrieveUserWithRolesAsync<TUser, TRole>(AuthUserId id)
			where TUser : AuthUserWithRoles<TRole>
			where TRole : IAuthRole =>
			from u in User.RetrieveAsync<TUser>(id)
			from r in Query.GetRolesForUserAsync<TRole>(u.Id)
			select u with { Roles = r };

		/// <inheritdoc/>
		public Task<Option<TUser>> RetrieveUserWithRolesAsync<TUser, TRole>(string email)
			where TUser : AuthUserWithRoles<TRole>
			where TRole : IAuthRole =>
			from u in User.RetrieveAsync<TUser>(email)
			from r in Query.GetRolesForUserAsync<TRole>(u.Id)
			select u with { Roles = r };

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Invalid password</summary>
			public sealed record class InvalidPasswordMsg : IMsg { }

			/// <summary>Null or empty email address</summary>
			public sealed record class NullOrEmptyEmailMsg : IMsg { }

			/// <summary>Null or empty password</summary>
			public sealed record class NullOrEmptyPasswordMsg : IMsg { }

			/// <summary>User not enabled</summary>
			public sealed record class UserNotEnabledMsg(string Value) : WithValueMsg<string> { }

			/// <summary>User not found</summary>
			public sealed record class UserNotFoundMsg(string EmailAddress) : NotFoundMsg { }
		}
	}
}
