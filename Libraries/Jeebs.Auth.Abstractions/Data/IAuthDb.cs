// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using JeebsF;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Database methods for authentication
	/// </summary>
	public interface IAuthDb
	{
		/// <summary>
		/// Get the authentication user to validate password etc
		/// </summary>
		/// <param name="email">User email address</param>
		Task<Option<TUser>> GetAuthUserAsync<TUser>(string email)
			where TUser : IUserAuth;

		/// <summary>
		/// Update the user's last sign in
		/// </summary>
		/// <param name="userId">User ID</param>
		Task<Option<bool>> UpdateUserLastSignIn(UserId userId);

		/// <summary>
		/// Get the specified user
		/// </summary>
		/// <typeparam name="TUserModel">User model</typeparam>
		/// <param name="userId">User ID</param>
		Task<Option<TUserModel>> GetUserAsync<TUserModel>(UserId userId)
			where TUserModel : IUserModel;
	}
}
