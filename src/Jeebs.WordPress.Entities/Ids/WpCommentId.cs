// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.WordPress.Entities
{
	/// <summary>
	/// WordPress Comment ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public sealed record WpCommentId(long Value) : StrongId(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public WpCommentId() : this(0) { }
	}
}
