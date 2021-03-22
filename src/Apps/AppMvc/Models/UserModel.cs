// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using Jeebs;
using Jeebs.Auth.Data;

namespace AppMvc.Models
{
	public record UserModel : IUserModel<RoleModel>, IAuthUser
	{
		public UserId UserId { get; init; } = new();
		public string EmailAddress { get; init; } = string.Empty;
		public string FriendlyName { get; init; } = string.Empty;
		public string FullName { get; init; } = string.Empty;
		public bool IsSuper { get; init; }
		public List<RoleModel> Roles { get; init; } = new();
		public string PasswordHash { get; init; } = string.Empty;
		public bool IsEnabled { get; init; }
		public DateTimeOffset? LastSignedIn { get; init; }
		public long Version { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
		public StrongId Id { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
	}
}
