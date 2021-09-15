// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Data;

namespace Jeebs.Auth.Data;

/// <summary>
/// Provides Authentication functions for interacting with User
/// </summary>
/// <typeparam name="TUserEntity">User Entity type</typeparam>
public interface IAuthUserRepository<TUserEntity> : IRepository<TUserEntity, AuthUserId>
	where TUserEntity : IAuthUser, IWithId
{
	/// <summary>
	/// Create a new User
	/// </summary>
	/// <param name="email">Email address</param>
	/// <param name="password">Password (will be hashed)</param>
	/// <param name="friendlyName">[Optional] Friendly name</param>
	Task<Option<AuthUserId>> CreateAsync(string email, string password, string? friendlyName);

	/// <summary>
	/// Retrieve a user by email address
	/// </summary>
	/// <param name="email">Email address</param>
	Task<Option<TModel>> RetrieveAsync<TModel>(string email);

	/// <summary>
	/// Update the user's last sign in to now
	/// </summary>
	/// <param name="userId">User ID</param>
	Task<Option<bool>> UpdateLastSignInAsync(AuthUserId userId);
}
