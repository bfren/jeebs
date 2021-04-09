// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using System.Threading.Tasks;
using Jeebs.Data;

namespace Jeebs.Auth.Data
{
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
		/// <param name="transaction">[Optional] Transaction</param>
		Task<Option<AuthUserId>> CreateAsync(string email, string password, string? friendlyName, IDbTransaction? transaction = null);

		/// <summary>
		/// Retrieve a user by email address
		/// </summary>
		/// <param name="email">Email address</param>
		Task<Option<TModel>> RetrieveAsync<TModel>(string email);

		/// <summary>
		/// Update the user's last sign in to now
		/// </summary>
		/// <param name="userId">User ID</param>
		/// <param name="transaction">[Optional] Transaction</param>
		Task<Option<bool>> UpdateLastSignInAsync(AuthUserId userId, IDbTransaction? transaction = null);
	}
}