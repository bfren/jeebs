// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Ids;
using Jeebs.Data.Common;
using Jeebs.Logging;

namespace Jeebs.Auth.Data;

/// <inheritdoc cref="IAuthRoleRepository"/>
public sealed class AuthRoleRepository : Repository<AuthRoleEntity, AuthRoleId>, IAuthRoleRepository
{
	/// <summary>
	/// Inject dependencies.
	/// </summary>
	/// <param name="db">IAuthDb.</param>
	/// <param name="log">ILog.</param>
	public AuthRoleRepository(IAuthDb db, ILog<AuthRoleRepository> log) : base(db, log) { }

	/// <inheritdoc/>
	public Task<Result<AuthRoleId>> CreateAsync(string name) =>
		CreateAsync(new AuthRoleEntity { Name = name });
}
