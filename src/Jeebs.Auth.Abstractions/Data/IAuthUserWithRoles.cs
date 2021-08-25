// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Authentication User with list of Roles
	/// </summary>
	/// <typeparam name="TRole">Role model type</typeparam>
	public interface IAuthUserWithRoles<TRole> : IWithId<AuthUserId>
		where TRole : IAuthRole
	{
		/// <summary>
		/// The roles this user is assigned to
		/// </summary>
		List<TRole> Roles { get; init; }
	}

	/// <inheritdoc cref="IAuthUserWithRoles{TRole}"/>
	public abstract record class AuthUserWithRoles<TRole> : IAuthUserWithRoles<TRole>
		where TRole : IAuthRole
	{
		/// <summary>
		/// User ID
		/// </summary>
		public AuthUserId Id { get; init; } = new();

		/// <summary>
		/// List of Roles
		/// </summary>
		public List<TRole> Roles { get; init; } = new();
	}
}
