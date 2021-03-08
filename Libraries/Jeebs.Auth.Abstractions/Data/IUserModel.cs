// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
		/// Whether or not the user account has super permissions
		/// </summary>
		bool IsSuper { get; init; }
	}
}
