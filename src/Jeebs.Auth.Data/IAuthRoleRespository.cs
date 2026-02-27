// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Ids;
using Jeebs.Data.Repository;

namespace Jeebs.Auth.Data;

/// <summary>
/// Auth Role Repository.
/// </summary>
public interface IAuthRoleRepository : IRepository<AuthRoleEntity, AuthRoleId>
{
	/// <summary>
	/// Create a new Role.
	/// </summary>
	/// <param name="name">Role name.</param>
	/// <returns>New Role ID.</returns>
	Task<Result<AuthRoleId>> CreateAsync(string name);
}
