// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Data;

namespace Jeebs.Auth.Data;

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
	Task<Option<AuthRoleId>> CreateAsync(string name);
}
