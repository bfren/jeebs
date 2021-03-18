// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// User model - allows consistent interaction in user interface
	/// </summary>
	public interface IUserModel : IUserWithUserId
	{
		/// <summary>
		/// Email address
		/// </summary>
		string EmailAddress { get; init; }

		/// <summary>
		/// Friendly name - option for user interface interaction
		/// </summary>
		string FriendlyName { get; init; }

		/// <summary>
		/// Whether or not the user account has super permissions
		/// </summary>
		bool IsSuper { get; init; }
	}

	/// <summary>
	/// User interface - supporting roles
	/// </summary>
	/// <typeparam name="TRole">Role type</typeparam>
	public interface IUserModel<TRole> : IUserModel
		where TRole : IRoleModel
	{
		/// <summary>
		/// The roles this user is assigned to
		/// </summary>
		public List<TRole> Roles { get; init; }
	}
}
