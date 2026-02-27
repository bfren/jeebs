// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Ids;
using Jeebs.Auth.Data.Models;
using Jeebs.Auth.Data.Tables;
using Jeebs.Cryptography;
using Jeebs.Data.Enums;

namespace Jeebs.Auth.Data;

public sealed class AuthDataProvider : IAuthDataProvider
{
	/// <inheritdoc/>
	public IAuthDb Db { get; private set; }

	/// <inheritdoc/>
	public IAuthUserRepository User { get; private init; }

	/// <inheritdoc/>
	public IAuthRoleRepository Role { get; private init; }

	/// <inheritdoc/>
	public IAuthUserRoleRepository UserRole { get; private init; }

	/// <summary>
	/// Inject dependencies.
	/// </summary>
	/// <param name="db">IAuthDb.</param>
	/// <param name="user">IAuthUserRepository.</param>
	/// <param name="role">IAuthRoleRepository.</param>
	/// <param name="userRole">IAuthUserRoleRepository.</param>
	public AuthDataProvider(
		IAuthDb db,
		IAuthUserRepository user,
		IAuthRoleRepository role,
		IAuthUserRoleRepository userRole
	) =>
		(Db, User, Role, UserRole) = (db, user, role, userRole);

	/// <inheritdoc/>
	public async Task<Result<bool>> ChangeUserPasswordAsync(AuthChangePasswordModel model)
	{
		// If the passwords don't match, do nothing
		if (model.NewPassword != model.CheckPassword)
		{
			return R.Fail("Passwords do not match.")
				.Ctx(nameof(AuthDataProvider), nameof(ChangeUserPasswordAsync));
		}

		// If the passwords are the same, do nothing
		if (model.NewPassword == model.CurrentPassword)
		{
			return R.Fail("New password cannot be the same as the old password.")
				.Ctx(nameof(AuthDataProvider), nameof(ChangeUserPasswordAsync));
		}

		// Get user for authentication
		return await User.RetrieveAsync<AuthUserEntity>(model.Id).BindAsync(async x =>
		{
			// Verify the entered password
			if (!x.PasswordHash.VerifyPassword(model.CurrentPassword))
			{
				return R.Fail("Invalid password.")
					.Ctx(nameof(AuthDataProvider), nameof(ChangeUserPasswordAsync));
			}

			// Update the password
			return await User.UpdateAsync(x with
			{
				Version = model.Version,
				PasswordHash = model.NewPassword.HashPassword()
			});
		});
	}

	/// <inheritdoc/>
	public Task<Result<List<TRole>>> GetRolesForUserAsync<TRole>(AuthUserId userId)
		where TRole : AuthRoleModel =>
		from r in Db.QueryAsync<TRole>(builder => builder
			.From<AuthRoleTable>()
			.Join<AuthRoleTable, AuthUserRoleTable>(QueryJoin.Inner, r => r.Id, ur => ur.RoleId)
			.Where<AuthUserRoleTable>(ur => ur.UserId, Compare.Equal, userId)
		)
		select r.ToList();

	/// <inheritdoc/>
	public Task<Result<TUser>> RetrieveUserAsync<TUser, TRole>(AuthUserId id)
		where TUser : AuthUserModel<TRole>
		where TRole : AuthRoleModel =>
		from u in User.RetrieveAsync<TUser>(id)
		from r in GetRolesForUserAsync<TRole>(u.Id)
		select u with { Roles = r };

	/// <inheritdoc/>
	public Task<Result<TUser>> RetrieveUserAsync<TUser, TRole>(string email)
		where TUser : AuthUserModel<TRole>
		where TRole : AuthRoleModel =>
		from u in User.RetrieveAsync<TUser>(email)
		from r in GetRolesForUserAsync<TRole>(u.Id)
		select u with { Roles = r };

	/// <inheritdoc/>
	public async Task<Result<bool>> ValidateUserAsync(string email, string password)
	{
		// Check email
		if (string.IsNullOrEmpty(email))
		{
			return R.Fail("Null or empty email.")
				.Ctx(nameof(AuthDataProvider), nameof(ValidateUserAsync));
		}

		// Check password
		if (string.IsNullOrEmpty(password))
		{
			return R.Fail("Invalid password.")
				.Ctx(nameof(AuthDataProvider), nameof(ValidateUserAsync));
		}

		// Check the user is enabled and the password is correct
		return await User.RetrieveAsync<AuthUserEntity>(email).BindAsync(x =>
		{
			if (!x.IsEnabled)
			{
				return R.Fail("User {Email} is not enabled.", email)
					.Ctx(nameof(AuthDataProvider), nameof(ValidateUserAsync));
			}

			if (!x.PasswordHash.VerifyPassword(password))
			{
				return R.Fail("Invalid password.")
					.Ctx(nameof(AuthDataProvider), nameof(ValidateUserAsync));
			}

			return R.True;
		});
	}
}
