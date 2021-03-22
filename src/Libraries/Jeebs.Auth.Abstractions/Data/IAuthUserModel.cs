// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Authentication User model
	/// </summary>
	public interface IAuthUserModel
	{
		/// <summary>
		/// User ID
		/// </summary>
		AuthUserId UserId { get; init; }

		/// <summary>
		/// Email address
		/// </summary>
		string EmailAddress { get; init; }

		/// <summary>
		/// Friendly name - option for user interface interaction
		/// </summary>
		string FriendlyName { get; init; }

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
	}

	/// <summary>
	/// User interface - supporting roles
	/// </summary>
	/// <typeparam name="TRole">Role type</typeparam>
	public interface IAuthUserModel<TRole> : IAuthUserModel
		where TRole : IAuthRoleModel
	{
		/// <summary>
		/// The roles this user is assigned to
		/// </summary>
		public List<TRole> Roles { get; init; }
	}
}
