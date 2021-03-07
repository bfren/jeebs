// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data;

namespace Jeebs.Auth
{
	/// <summary>
	/// User authentication provider
	/// </summary>
	public interface IDataAuthProvider
	{
		/// <summary>
		/// Validate a username and password
		/// </summary>
		/// <typeparam name="TUserModel">User model type</typeparam>
		/// <param name="email">Email (username)</param>
		/// <param name="password">Password</param>
		Task<Option<TUserModel>> ValidateUserAsync<TUserModel>(string email, string password)
			where TUserModel : IUserModel, new();
	}
}
