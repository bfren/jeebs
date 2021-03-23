// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Provides Authentication database functionality
	/// </summary>
	public interface IAuthDataProvider
	{
		#region Users

		/// <summary>
		/// Validate a User based on their email and password
		/// </summary>
		/// <typeparam name="TModel">Create Auth User Model type</typeparam>
		/// <param name="email">Email address</param>
		/// <param name="password">Password entered by the user</param>
		Task<Option<TModel>> ValidateUserAsync<TModel>(string email, string password)
		where TModel : IAuthUserModel;

		/// <summary>
		/// Update the user's last sign in to now
		/// </summary>
		/// <param name="userId">User ID</param>
		Task<Option<bool>> UpdateUserLastSignInAsync(AuthUserId userId);

		#endregion
	}
}