// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// WordPress User Meta ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public sealed record WpUserMetaId(long Value) : LongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public WpUserMetaId() : this(Default) { }
	}
}
