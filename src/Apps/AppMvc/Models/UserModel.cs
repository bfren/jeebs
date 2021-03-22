// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using Jeebs;
using Jeebs.Auth.Data;

namespace AppMvc.Models
{
	public record UserModel : IAuthUser<RoleModel>
	{
		public string PasswordHash { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
		public bool IsEnabled { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
		public DateTimeOffset? LastSignedIn { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
		public StrongId Id { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
		public List<RoleModel> Roles { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
		public AuthUserId UserId { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
		public string EmailAddress { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
		public string FriendlyName { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
		public string? GivenName { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
		public string? FamilyName { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
		public bool IsSuper { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
	}
}
