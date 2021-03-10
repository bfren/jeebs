// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Id;

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// Role ID
	/// </summary>
	/// <param name="Value">Id Value</param>
	public sealed record RoleId(long Value) : LongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public RoleId() : this(Default) { }
	}
}
