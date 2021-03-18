// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Auth.Data;

namespace AppMvc.Models
{
	public record RoleModel : IRoleModel
	{
		public RoleId RoleId { get; init; } = new();
		public string Name { get; init; } = string.Empty;
	}
}
