// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Data;
using Jeebs.Id;

namespace Jeebs.Auth.Data;

/// <summary>
/// Provides Authentication functions for interacting with User
/// </summary>
/// <typeparam name="TUserEntity">User Entity type</typeparam>
public interface IAuthUserRepository<TUserEntity> : IRepository<TUserEntity, AuthUserId>
	where TUserEntity : IAuthUser, IWithId
{
	/// <inheritdoc cref="CreateAsync(string, string, string)"/>
	Task<Maybe<AuthUserId>> CreateAsync(string email, string plainTextPassword);

	/// <summary>
	/// Create a new User
	/// </summary>
	/// <param name="email">Email address (used for login)</param>
	/// <param name="plainTextPassword">Password (will be hashed)</param>
	/// <param name="friendlyName">[Optional] The user's actual name</param>
	Task<Maybe<AuthUserId>> CreateAsync(string email, string plainTextPassword, string? friendlyName);

	/// <summary>
	/// Retrieve a user by email address
	/// </summary>
	/// <typeparam name="TModel">User model type</typeparam>
	/// <param name="email">Email address</param>
	Task<Maybe<TModel>> RetrieveAsync<TModel>(string email);

	/// <summary>
	/// Update the user's last sign in to now
	/// </summary>
	/// <param name="userId">User ID</param>
	Task<Maybe<bool>> UpdateLastSignInAsync(AuthUserId userId);
}
