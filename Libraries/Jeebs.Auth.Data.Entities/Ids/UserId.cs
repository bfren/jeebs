// Copyright (c) bcg|design.
// Licensed under https://bcg.mit-license.org/2013.

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// User ID
	/// </summary>
	/// <param name="Value">Id Value</param>
	public sealed record UserId(long Value) : LongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public UserId() : this(Default) { }
	}
}
