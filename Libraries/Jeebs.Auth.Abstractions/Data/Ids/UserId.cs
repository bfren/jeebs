// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Auth.Data
{
	/// <summary>
	/// User ID
	/// </summary>
	/// <param name="Value">Id Value</param>
	public sealed record UserId(long Value) : Id.LongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public UserId() : this(Default) { }
	}
}
