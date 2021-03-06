// Copyright (c) bcg|design.
// Licensed under https://bcg.mit-license.org/2013.

using System;
using Jeebs.Data;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// User interface
	/// </summary>
	internal interface IUser : IUserModel, IEntity<long>
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
		/// Whether or not the user account is enabled
		/// </summary>
		bool IsEnabled { get; init; }

		/// <summary>
		/// The last time the user signed in
		/// </summary>
		DateTime? LastSignedIn { get; init; }
	}
}