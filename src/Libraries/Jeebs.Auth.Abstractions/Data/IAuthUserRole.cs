// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Authentication User Role Model
	/// </summary>
	public interface IAuthUserRole : IWithId<AuthUserRoleId>
	{
		/// <summary>
		/// User ID
		/// </summary>
		AuthUserId UserId { get; init; }

		/// <summary>
		/// Role ID
		/// </summary>
		AuthRoleId RoleId { get; init; }
	}
}
