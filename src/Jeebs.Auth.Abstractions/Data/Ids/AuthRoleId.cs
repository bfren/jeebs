// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Role ID
	/// </summary>
	public sealed record class AuthRoleId(ulong Value) : StrongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public AuthRoleId() : this(0) { }
	}
}
