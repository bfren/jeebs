// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Used for creating an Authenticated user
	/// </summary>
	public interface IAuthCreateUserModel
	{
		/// <summary>
		/// Email address
		/// </summary>
		string EmailAddress { get; init; }

		/// <summary>
		/// Password
		/// </summary>
		string Password { get; init; }
	}
}
