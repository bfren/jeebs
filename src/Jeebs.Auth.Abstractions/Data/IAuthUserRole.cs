// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
