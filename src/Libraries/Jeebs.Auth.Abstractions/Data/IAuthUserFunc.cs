// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Jeebs.Data;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Authentication User CRUD functions
	/// </summary>
	/// <typeparam name="TUser">User Type</typeparam>
	public interface IAuthUserFunc<TUser> : IDbFunc<TUser, AuthUserId>
		where TUser : IAuthUser
	{
		/// <summary>
		/// Get the authentication user to validate password etc
		/// </summary>
		/// <param name="email">User email address</param>
		Task<Option<TUser>> RetrieveAsync(string email);

		/// <summary>
		/// Update the user's last sign in
		/// </summary>
		/// <param name="userId">User ID</param>
		Task<Option<bool>> UpdateLastSignInAsync(AuthUserId userId);
	}
}
