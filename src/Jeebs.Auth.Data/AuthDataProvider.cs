// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Models;
using Jeebs.Cryptography;
using Jeebs.Messages;

namespace Jeebs.Auth.Data;

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
	/// Inject dependencies.
	/// </summary>
	/// <param name="user">IAuthUserRepository.</param>
	/// <param name="role">IAuthRoleRepository.</param>
	/// <param name="userRole">IAuthUserRoleRepository.</param>
	/// <param name="query">IAuthDbQuery.</param>
	public AuthDataProvider(
		IAuthUserRepository user,
		IAuthRoleRepository role,
		IAuthUserRoleRepository userRole,
		IAuthDbQuery query
	) =>
		(User, Role, UserRole, Query) = (user, role, userRole, query);

	/// <inheritdoc/>
	public async Task<Maybe<TModel>> ValidateUserAsync<TModel>(string email, string password)
		where TModel : IAuthUser
	{
		// Check email
		if (string.IsNullOrEmpty(email))
		{
			return F.None<TModel, M.NullOrEmptyEmailMsg>();
		}

		// Check password
		if (string.IsNullOrEmpty(password))
		{
			return F.None<TModel, M.InvalidPasswordMsg>();
		}

		// Get user for authentication
		foreach (var user in await User.RetrieveAsync<AuthUserEntity>(email).ConfigureAwait(false))
		{
			// Verify the user is enabled
			if (!user.IsEnabled)
			{
				return F.None<TModel>(new M.UserNotEnabledMsg(email));
			}

			// Verify the entered password
			if (!user.PasswordHash.VerifyPassword(password))
			{
				return F.None<TModel, M.InvalidPasswordMsg>();
			}

			// Get user model
			return await User.RetrieveAsync<TModel>(user.Id).ConfigureAwait(false);
		}

		// User not found
		return F.None<TModel>(new M.UserEmailNotFoundMsg(email));
	}

	/// <inheritdoc/>
	public Task<Maybe<TUser>> RetrieveUserWithRolesAsync<TUser, TRole>(AuthUserId id)
		where TUser : AuthUserWithRoles<TRole>
		where TRole : IAuthRole =>
		from u in User.RetrieveAsync<TUser>(id)
		from r in Query.GetRolesForUserAsync<TRole>(u.Id)
		select u with { Roles = r };

	/// <inheritdoc/>
	public Task<Maybe<TUser>> RetrieveUserWithRolesAsync<TUser, TRole>(string email)
		where TUser : AuthUserWithRoles<TRole>
		where TRole : IAuthRole =>
		from u in User.RetrieveAsync<TUser>(email)
		from r in Query.GetRolesForUserAsync<TRole>(u.Id)
		select u with { Roles = r };

	/// <inheritdoc/>
	public async Task<Maybe<bool>> ChangeUserPasswordAsync(AuthChangePasswordModel model)
	{
		// If the passwords don't match, do nothing
		if (model.NewPassword != model.CheckPassword)
		{
			return F.None<bool, M.PasswordsDoNotMatchMsg>();
		}

		// If the passwords are the same, do nothing
		if (model.NewPassword == model.CurrentPassword)
		{
			return F.None<bool, M.NewPasswordIsNotDifferentMsg>();
		}

		// Get user for authentication
		foreach (var user in await User.RetrieveAsync<AuthUserEntity>(model.Id).ConfigureAwait(false))
		{
			// Verify the entered password
			if (!user.PasswordHash.VerifyPassword(model.CurrentPassword))
			{
				return F.None<bool, M.InvalidPasswordMsg>();
			}

			// Update the password
			return await User.UpdateAsync(user with
			{
				Version = model.Version,
				PasswordHash = model.NewPassword.HashPassword()
			});
		}

		// User not found
		return F.None<bool>(new M.UserIdNotFoundMsg(model.Id));
	}

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>Invalid password</summary>
		public sealed record class InvalidPasswordMsg : Msg;

		/// <summary>Null or empty email address</summary>
		public sealed record class NullOrEmptyEmailMsg : Msg;

		/// <summary>Null or empty password</summary>
		public sealed record class NullOrEmptyPasswordMsg : Msg;

		/// <summary>User not enabled</summary>
		/// <param name="Value">Email address.</param>
		public sealed record class UserNotEnabledMsg(string Value) : WithValueMsg<string>;

		/// <summary>User not found</summary>
		/// <param name="Value">User Id.</param>
		public sealed record class UserIdNotFoundMsg(AuthUserId Value) : NotFoundMsg<AuthUserId>
		{
			/// <inheritdoc/>
			public override string Name =>
				"EmailAddress";
		}

		/// <summary>User not found</summary>
		/// <param name="Value">Email address.</param>
		public sealed record class UserEmailNotFoundMsg(string Value) : NotFoundMsg<string>
		{
			/// <inheritdoc/>
			public override string Name =>
				"EmailAddress";
		}

		/// <summary>Passwords don't match</summary>
		public sealed record class PasswordsDoNotMatchMsg : Msg;

		/// <summary>New password is the same as the current password</summary>
		public sealed record class NewPasswordIsNotDifferentMsg : Msg;
	}
}
