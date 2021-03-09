// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Data;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// User properties used during authentication
	/// </summary>
	public interface IUserAuth : IEntity<long>
	{
		/// <summary>
		/// User ID
		/// </summary>
		UserId UserId { get; init; }

		/// <summary>
		/// Email address
		/// </summary>
		string EmailAddress { get; init; }

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
		DateTime? LastSignedIn { get; init; }
	}
}
