// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Data;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// User interface
	/// </summary>
	public interface IUser : IUserModel, IEntity<long>
	{
		/// <summary>
		/// The user's encrypted password
		/// </summary>
		string PasswordHash { get; init; }

		/// <summary>
		/// Given (Christian / first) name
		/// </summary>
		string GivenName { get; init; }

		/// <summary>
		/// Family name (surname)
		/// </summary>
		string FamilyName { get; init; }

		/// <summary>
		/// Full name - normally GivenName + FamilyName
		/// </summary>
		string FullName { get; }

		/// <summary>
		/// Whether or not the user account is enabled
		/// </summary>
		bool IsEnabled { get; init; }

		/// <summary>
		/// The last time the user signed in
		/// </summary>
		DateTime? LastSignedIn { get; init; }
	}

	/// <summary>
	/// User interface - supporting roles
	/// </summary>
	/// <typeparam name="TRole">Role type</typeparam>
	public interface IUser<TRole> : IUser, IUserModel<TRole>
		where TRole : IRole
	{
	}
}
