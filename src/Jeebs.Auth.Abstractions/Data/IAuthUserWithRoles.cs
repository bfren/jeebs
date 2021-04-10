﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;

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
	public abstract record AuthUserWithRoles<TRole> : IAuthUserWithRoles<TRole>
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
