// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Ids;
using Jeebs.Data.Common;

namespace Jeebs.Auth.Data;

/// <summary>
/// Auth User Repository.
/// </summary>
public interface IAuthUserRepository : IRepository<AuthUserEntity, AuthUserId>
{
	/// <inheritdoc cref="CreateAsync(string, string, string?)"/>
	Task<Result<AuthUserId>> CreateAsync(string email, string plainTextPassword);

	/// <summary>
	/// Create a new User.
	/// </summary>
	/// <param name="email">Email address (used for login).</param>
	/// <param name="plainTextPassword">Password (will be hashed).</param>
	/// <param name="friendlyName">[Optional] The user's actual name.</param>
	/// <returns>New User's ID.</returns>
	Task<Result<AuthUserId>> CreateAsync(string email, string plainTextPassword, string? friendlyName);

	/// <summary>
	/// Retrieve a user by email address.
	/// </summary>
	/// <typeparam name="TModel">User model type.</typeparam>
	/// <param name="email">Email address.</param>
	/// <returns>User Model.</returns>
	Task<Result<TModel>> RetrieveAsync<TModel>(string email);

	/// <summary>
	/// Update the user's last sign in to now.
	/// </summary>
	/// <param name="userId">User ID.</param>
	/// <returns>Success or failure.</returns>
	Task<Result<bool>> UpdateLastSignInAsync(AuthUserId userId);
}
