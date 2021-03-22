// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs;
using Jeebs.Auth.Data;

namespace AppMvc.Models
{
	public record RoleModel : IAuthRole
	{
		public AuthRoleId RoleId { get; init; } = new();
		public string Name { get; init; } = string.Empty;
		public string Description { get => throw new System.NotImplementedException(); init => throw new System.NotImplementedException(); }
		public StrongId Id { get => throw new System.NotImplementedException(); init => throw new System.NotImplementedException(); }
	}
}
