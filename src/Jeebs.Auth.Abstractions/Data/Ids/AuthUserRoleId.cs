// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// User Role ID
	/// </summary>
	public sealed record class AuthUserRoleId(ulong Value) : StrongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public AuthUserRoleId() : this(0) { }
	}
}
