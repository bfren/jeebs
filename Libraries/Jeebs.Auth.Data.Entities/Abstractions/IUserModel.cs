// Copyright (c) bcg|design.
// Licensed under https://bcg.mit-license.org/2013.

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// User model - allows consistent interaction in user interface
	/// </summary>
	public interface IUserModel
	{
		/// <summary>
		/// User ID
		/// </summary>
		public UserId UserId { get; init; }

		/// <summary>
		/// Email address
		/// </summary>
		string EmailAddress { get; init; }

		/// <summary>
		/// Friendly name - option for user interface interaction
		/// </summary>
		string FriendlyName { get; init; }

		/// <summary>
		/// Full name - normally GivenName + FamilyName
		/// </summary>
		string FullName { get; }

		/// <summary>
		/// Whether or not the user account has super permissions
		/// </summary>
		bool IsSuper { get; init; }
	}
}
