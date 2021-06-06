// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Data;
using System.Threading.Tasks;
using Jeebs.Data;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Provides Authentication functions for interacting with Roles
	/// </summary>
	/// <typeparam name="TRoleEntity">Role Entity type</typeparam>
	public interface IAuthRoleRepository<TRoleEntity> : IRepository<TRoleEntity, AuthRoleId>
		where TRoleEntity : IAuthRole, IWithId
	{
		/// <summary>
		/// Create a new Role
		/// </summary>
		/// <param name="name">Role name</param>
		/// <param name="transaction">[Optional] Transaction</param>
		Task<Option<AuthRoleId>> CreateAsync(string name, IDbTransaction? transaction = null);
	}
}