// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using System.Threading.Tasks;
using Jeebs.Data;
using StrongId;

namespace Jeebs.Auth.Data;

/// <summary>
/// Provides Authentication functions for interacting with User.
/// </summary>
/// <typeparam name="TUserEntity">User Entity type</typeparam>
public interface IAuthUserRepository<TUserEntity> : IRepository<TUserEntity, AuthUserId>
	where TUserEntity : IAuthUser, IWithId
{
	/// <inheritdoc cref="CreateAsync(string, string, string?, IDbTransaction)"/>
	Task<Maybe<AuthUserId>> CreateAsync(string email, string plainTextPassword);

	/// <inheritdoc cref="CreateAsync(string, string, string?, IDbTransaction)"/>
	Task<Maybe<AuthUserId>> CreateAsync(string email, string plainTextPassword, IDbTransaction transaction);

	/// <inheritdoc cref="CreateAsync(string, string, string?, IDbTransaction)"/>
	Task<Maybe<AuthUserId>> CreateAsync(string email, string plainTextPassword, string? friendlyName);

	/// <summary>
	/// Create a new User
	/// </summary>
	/// <param name="email">Email address (used for login)</param>
	/// <param name="plainTextPassword">Password (will be hashed)</param>
	/// <param name="friendlyName">[Optional] The user's actual name</param>
	/// <param name="transaction">IDbTransaction</param>
	Task<Maybe<AuthUserId>> CreateAsync(string email, string plainTextPassword, string? friendlyName, IDbTransaction transaction);

	/// <inheritdoc cref="RetrieveAsync{TModel}(string, IDbTransaction)"/>
	Task<Maybe<TModel>> RetrieveAsync<TModel>(string email);

	/// <summary>
	/// Retrieve a user by email address
	/// </summary>
	/// <typeparam name="TModel">User model type</typeparam>
	/// <param name="email">Email address</param>
	/// <param name="transaction">IDbTransaction</param>
	Task<Maybe<TModel>> RetrieveAsync<TModel>(string email, IDbTransaction transaction);

	/// <inheritdoc cref="UpdateLastSignInAsync(AuthUserId, IDbTransaction)"/>
	Task<Maybe<bool>> UpdateLastSignInAsync(AuthUserId userId);

	/// <summary>
	/// Update the user's last sign in to now
	/// </summary>
	/// <param name="userId">User ID</param>
	/// <param name="transaction">IDbTransaction</param>
	Task<Maybe<bool>> UpdateLastSignInAsync(AuthUserId userId, IDbTransaction transaction);
}
