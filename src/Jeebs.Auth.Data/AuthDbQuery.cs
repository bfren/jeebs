// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Tables;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying;
using static F.OptionF;

namespace Jeebs.Auth;

/// <inheritdoc cref="IAuthDbQuery"/>
public sealed class AuthDbQuery : DbQuery<IAuthDb>, IAuthDbQuery
{
	/// <summary>
	/// Inject dependencies
	/// </summary>
	/// <param name="db">IAuthDb</param>
	/// <param name="log">ILog</param>
	public AuthDbQuery(IAuthDb db, ILog<AuthDbQuery> log) : base(db, log) { }

	/// <inheritdoc/>
	public Task<Option<List<TRole>>> GetRolesForUserAsync<TRole>(AuthUserId userId)
		where TRole : IAuthRole =>
		Some(userId)
		.BindAsync(
			x => this.QueryAsync<TRole>(builder => builder
				.From<AuthRoleTable>()
				.Join<AuthRoleTable, AuthUserRoleTable>(QueryJoin.Inner, r => r.Id, ur => ur.RoleId)
				.Where<AuthUserRoleTable>(ur => ur.UserId, Compare.Equal, x.Value)
			)
		)
		.MapAsync(
			x => x.ToList(),
			DefaultHandler
		);
}
