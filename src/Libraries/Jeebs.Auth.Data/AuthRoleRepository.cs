// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Data;
using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Data;

namespace Jeebs.Auth
{
	/// <inheritdoc cref="IAuthRoleRepository{TRoleEntity}"/>
	public interface IAuthRoleRepository : IAuthRoleRepository<AuthRoleEntity>
	{ }

	/// <inheritdoc cref="IAuthRoleRepository{TRoleEntity}"/>
	public sealed class AuthRoleRepository : Repository<AuthRoleEntity, AuthRoleId>, IAuthRoleRepository
	{
		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="db">IAuthDb</param>
		/// <param name="log">ILog</param>
		public AuthRoleRepository(IAuthDb db, ILog<AuthRoleRepository> log) : base(db, log) { }

		/// <inheritdoc/>
		public Task<Option<AuthRoleId>> CreateAsync(string name, IDbTransaction? transaction = null) =>
			CreateAsync(new AuthRoleEntity { Name = name }, transaction);
	}
}
