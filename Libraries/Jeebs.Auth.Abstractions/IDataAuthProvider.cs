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
	public interface IDataAuthProvider<TUser>
		where TUser : IAuthUser, IUserModel
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
	public interface IDataAuthProvider<TUser, TRole> : IDataAuthProvider<TUser>
		where TUser : IAuthUser, IUserModel<TRole>
		where TRole : IRoleModel
	{
		/// <summary>
		/// Validate a username and password
		/// </summary>
		/// <param name="email">Email (username)</param>
		/// <param name="password">Password</param>
		new Task<Option<TUser>> ValidateUserAsync(string email, string password);
	}
}
