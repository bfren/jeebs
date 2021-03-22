// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Auth.Data.Entities;
using Jeebs.Data;

namespace Jeebs.Auth.Data.Tables
{
	/// <summary>
	/// Role Table
	/// </summary>
	public sealed record RoleTable : Table
	{
		/// <summary>
		/// Prefix added before all columns
		/// </summary>
		public const string Prefix = "Role";

		/// <inheritdoc cref="RoleEntity.RoleId"/>
		public string RoleId =>
			nameof(RoleId);

		/// <inheritdoc cref="RoleEntity.Name"/>
		public string Name =>
			Prefix + nameof(Name);

		/// <inheritdoc cref="RoleEntity.Description"/>
		public string Description =>
			Prefix + nameof(Description);

		/// <summary>
		/// Create object
		/// </summary>
		public RoleTable() : base("auth_role") { }
	}
}
