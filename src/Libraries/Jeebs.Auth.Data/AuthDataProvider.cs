// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Cryptography;
using Jeebs.Data.Enums;
using Jeebs.Linq;
using static F.OptionF;

namespace Jeebs.Auth
{
	public interface IAuthDataProvider : IAuthDataProvider<AuthUserEntity, AuthRoleEntity, AuthUserRoleEntity>
	{ }

	/// <inheritdoc cref="IAuthDataProvider{TUserEntity, TRoleEntity, TUserRoleEntity}"/>
	public sealed class AuthDataProvider : IAuthDataProvider
	{
		/// <inheritdoc/>
		public IAuthUserFunc<AuthUserEntity> User { get; private init; }

		/// <inheritdoc/>
		public IAuthRoleFunc<AuthRoleEntity> Role { get; private init; }

		/// <inheritdoc/>
		public IAuthUserRoleFunc<AuthUserRoleEntity> UserRole { get; private init; }

		#region Users

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="user">IAuthUserFunc</param>
		/// <param name="role">IAuthRoleFunc</param>
		/// <param name="userRole">IAuthUserRoleFunc</param>
		public AuthDataProvider(IAuthUserFunc user, IAuthRoleFunc role, IAuthUserRoleFunc userRole) =>
			(User, Role, UserRole) = (user, role, userRole);

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
			from r in RetrieveRolesForUserAsync<TRole>(u.Id)
			select u with { Roles = r };

		/// <inheritdoc/>
		public Task<Option<TUser>> RetrieveUserWithRolesAsync<TUser, TRole>(string email)
			where TUser : AuthUserWithRoles<TRole>
			where TRole : IAuthRole =>
			from u in User.RetrieveAsync<TUser>(email)
			from r in RetrieveRolesForUserAsync<TRole>(u.Id)
			select u with { Roles = r };

		#endregion

		#region Roles

		/// <inheritdoc/>
		public Task<Option<List<TRole>>> RetrieveRolesForUserAsync<TRole>(AuthUserId userId)
			where TRole : IAuthRole =>
			Return(
				userId
			)
			.BindAsync(
				userId => UserRole.QueryAsync<AuthUserRoleEntity>(
					(ur => ur.UserId, SearchOperator.Equal, userId.Value)
				)
			)
			.BindAsync(
				userRoles => Role.QueryAsync<TRole>(
					(r => r.Id, SearchOperator.In, string.Join(",", userRoles.Select(ur => ur.RoleId)))
				)
			)
			.MapAsync(
				roles => roles.ToList(),
				DefaultHandler
			);

		#endregion

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
