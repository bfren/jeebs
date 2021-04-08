// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// WordPress Comment Meta ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public sealed record WpCommentMetaId(long Value) : StrongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public WpCommentMetaId() : this(0) { }
	}
}
