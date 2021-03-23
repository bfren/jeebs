// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Jeebs.Data;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Provides Authentication database functionality
	/// </summary>
	public interface IAuthDataProvider
	{
		/// <summary>
		/// Validate a User based on their email and password
		/// </summary>
		/// <typeparam name="TModel">Create Auth User Model type</typeparam>
		/// <param name="email">Email address</param>
		/// <param name="password">Password entered by the user</param>
		Task<Option<TModel>> ValidateUserAsync<TModel>(string email, string password)
			where TModel : IAuthUserModel;

		/// <summary>
		/// Create a user from the given model
		/// </summary>
		/// <typeparam name="TModel">Auth User Model type</typeparam>
		/// <param name="user">User</param>
		Task<Option<AuthUserId>> CreateUserAsync<TModel>(TModel user)
			where TModel : IAuthCreateUserModel;

		/// <summary>
		/// Retrieve a user with the specified ID
		/// </summary>
		/// <typeparam name="TModel">Auth User Model type</typeparam>
		/// <param name="id">User ID</param>
		Task<Option<TModel>> RetrieveUserAsync<TModel>(AuthUserId id);

		/// <summary>
		/// Update a user with the given model
		/// </summary>
		/// <typeparam name="TModel">Auth User Model type</typeparam>
		/// <param name="user">User</param>
		Task<Option<bool>> UpdateUserAsync<TModel>(TModel user)
			where TModel : IWithId;
	}
}