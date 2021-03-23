// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Jeebs.Data;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Authentication User model
	/// </summary>
	public interface IAuthUserModel : IWithId<AuthUserId>
	{
		/// <summary>
		/// Email address
		/// </summary>
		string EmailAddress { get; init; }

		/// <summary>
		/// Friendly name - option for user interface interaction
		/// </summary>
		string? FriendlyName { get; init; }

		/// <summary>
		/// Given (Christian / first) name
		/// </summary>
		string? GivenName { get; init; }

		/// <summary>
		/// Family name (surname)
		/// </summary>
		string? FamilyName { get; init; }

		/// <summary>
		/// Whether or not the user account has super permissions
		/// </summary>
		bool IsSuper { get; init; }

		/// <summary>
		/// The roles this user is assigned to
		/// </summary>
		List<IAuthRoleModel> Roles { get; init; }
	}
}
