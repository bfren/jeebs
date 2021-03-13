// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// User interface
	/// </summary>
	public interface IUser : IAuthUser, IUserModel
	{
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
