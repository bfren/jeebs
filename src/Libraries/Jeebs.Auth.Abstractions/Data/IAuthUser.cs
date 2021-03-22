// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Data;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Authentication User
	/// </summary>
	public interface IAuthUser : IAuthUserModel, IEntity
	{
		/// <summary>
		/// The user's encrypted password
		/// </summary>
		string PasswordHash { get; init; }

		/// <summary>
		/// Whether or not the user account is enabled
		/// </summary>
		bool IsEnabled { get; init; }

		/// <summary>
		/// The last time the user signed in
		/// </summary>
		DateTimeOffset? LastSignedIn { get; init; }
	}

	public interface IAuthUser<TRole> : IAuthUser, IAuthUserModel<TRole>
		where TRole : IAuthRole
	{ }
}
