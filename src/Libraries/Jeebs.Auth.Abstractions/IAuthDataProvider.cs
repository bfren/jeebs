// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data;

namespace Jeebs.Auth
{
	/// <summary>
	/// Data context User authentication provider
	/// </summary>
	/// <typeparam name="TUser">User model type</typeparam>
	public interface IAuthDataProvider<TUser>
		where TUser : IAuthUser
	{
		/// <summary>
		/// Validate a username and password
		/// </summary>
		/// <param name="email">Email (username)</param>
		/// <param name="password">Password</param>
		Task<Option<TUser>> ValidateUserAsync(string email, string password);
	}

	/// <summary>
	/// Data context User authentication provider with Role support
	/// </summary>
	/// <typeparam name="TUser">User model type</typeparam>
	/// <typeparam name="TRole">Role model type</typeparam>
	public interface IDataAuthProvider<TUser, TRole> : IAuthDataProvider<TUser>
		where TUser : IAuthUser<TRole>
		where TRole : IAuthRole
	{
		/// <summary>
		/// Validate a username and password
		/// </summary>
		/// <param name="email">Email (username)</param>
		/// <param name="password">Password</param>
		new Task<Option<TUser>> ValidateUserAsync(string email, string password);
	}
}
