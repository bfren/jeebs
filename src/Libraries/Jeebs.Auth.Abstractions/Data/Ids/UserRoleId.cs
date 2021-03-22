// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// User Role ID
	/// </summary>
	/// <param name="Value">Id Value</param>
	public sealed record UserRoleId(long Value) : StrongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public UserRoleId() : this(0) { }
	}
}
