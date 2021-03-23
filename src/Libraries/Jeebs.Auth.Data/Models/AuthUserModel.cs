﻿// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using Jeebs.Data;

namespace Jeebs.Auth.Data.Models
{
	/// <summary>
	/// Authentication User model
	/// </summary>
	public record AuthUserModel : IAuthUserModel
	{
		/// <summary>
		/// User ID
		/// </summary>
		[Id]
		public AuthUserId Id { get; init; }

		/// <summary>
		/// Email address
		/// </summary>
		public string EmailAddress { get; init; }

		/// <summary>
		/// Friendly name - option for user interface interaction
		/// </summary>
		public string? FriendlyName { get; init; }

		/// <summary>
		/// Given (Christian / first) name
		/// </summary>
		public string? GivenName { get; init; }

		/// <summary>
		/// Family name (surname)
		/// </summary>
		public string? FamilyName { get; init; }

		/// <summary>
		/// Whether or not the user account has super permissions
		/// </summary>
		public bool IsSuper { get; init; }

		/// <summary>
		/// The roles this user is assigned to
		/// </summary>
		[Ignore]
		public List<IAuthRoleModel> Roles { get; init; } = new();

		/// <summary>
		/// Create with default values
		/// </summary>
		public AuthUserModel() : this(new(), string.Empty) { }

		/// <summary>
		/// Create with specified values
		/// </summary>
		/// <param name="id">AuthUserId</param>
		/// <param name="email">Email address</param>
		public AuthUserModel(AuthUserId id, string email) =>
			(Id, EmailAddress) = (id, email);
	}
}
