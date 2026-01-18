// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Jeebs.Auth.Data.Entities;
using Jeebs.Data;
using Jeebs.Logging;

namespace Jeebs.Auth.Data;

/// <inheritdoc cref="IAuthRoleRepository{TRoleEntity}"/>
public interface IAuthRoleRepository : IAuthRoleRepository<AuthRoleEntity>
{ }

/// <inheritdoc cref="IAuthRoleRepository{TRoleEntity}"/>
public sealed class AuthRoleRepository : Repository<AuthRoleEntity, AuthRoleId>, IAuthRoleRepository
{
	/// <summary>
	/// Inject dependencies.
	/// </summary>
	/// <param name="db">IAuthDb.</param>
	/// <param name="log">ILog.</param>
	public AuthRoleRepository(IAuthDb db, ILog<AuthRoleRepository> log) : base(db, log) { }

	/// <inheritdoc/>
	public Task<Maybe<AuthRoleId>> CreateAsync(string name) =>
		CreateAsync(new AuthRoleEntity { Name = name });
}
