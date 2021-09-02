// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Adds additional Authentication functionality to the base <see cref="IDbQuery"/>
	/// </summary>
	public interface IAuthDbQuery : IDbQuery
	{
		/// <summary>
		/// Retrieve the Roles added to the specified User
		/// </summary>
		/// <typeparam name="TRole">Role type</typeparam>
		/// <param name="userId">User ID</param>
		Task<Option<List<TRole>>> GetRolesForUserAsync<TRole>(AuthUserId userId)
			where TRole : IAuthRole;
	}
}
